using System.Collections.Generic;
using H3_Shoothouse.Classes;
using UnityEngine;
using UnityEngine.UI;

namespace H3_Shoothouse.MappingComponents
{
    public class ShoothouseScoreboard : MonoBehaviour
    {
        public GameObject StageRowPrefab;
        public GameObject StageRowParent;
        public Text FinalText;

        public void UpdateScoreboard(StageBreakdown[] breakdown, float totalTime)
        {
            // Clear whatever rows were there previously
            foreach (Transform child in StageRowParent.transform) Destroy(child.gameObject);
            
            // Create a new row on the board for each stage
            int i = 0, finalScore = 0;
            var stageTimes = 0f;
            foreach (var stage in breakdown)
            {
                // Instantiate it and take a reference to the reference holder
                var row = Instantiate(StageRowPrefab, StageRowParent.transform);
                var components = row.GetComponent<ShoothouseScoreboardRow>();

                // Set all the values
                if (components.ScoreText) components.ScoreText.text = $"{stage.Score}";
                if (components.StageName) components.StageName.text = $"Stage {stage.StageNumber}: {stage.StageName}";
                if (components.TimeText) components.TimeText.text = $" {stage.TimeTaken:##.000}s";
                if (components.TargetText) components.TargetText.text = $"x{stage.TargetsHit}";
                if (components.AntiTargetText) components.AntiTargetText.text = $"x{stage.AntiTargetsHit}";
                
                // Then move it down a bit
                var rectTransform = row.GetComponent<RectTransform>();
                var currentPos = rectTransform.anchoredPosition;
                currentPos.y -= rectTransform.sizeDelta.y * 1.05f * i;
                rectTransform.anchoredPosition = currentPos;
                
                // Then update the final score and iterator
                ++i;
                finalScore += stage.Score;
                stageTimes += stage.TimeTaken;
            }
            
            // Account for how long the player spent between completing the last stage and crossing the end trigger
            var timeDelta = totalTime - stageTimes;
            finalScore -= (int) (timeDelta * 50f);
            
            // Lastly update the final score
            FinalText.text = $"Time: <color=blue>{totalTime:##.000}</color>  Score: <color=blue>{finalScore:N0}</color>";
        }
    }
}