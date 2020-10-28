using System.Collections;
using System.Collections.Generic;
using System.Linq;
using H3VR_Kill_House.Classes;
using UnityEngine;
using UnityEngine.Events;

namespace H3VR_Kill_House.MappingComponents
{
    public class KillHouseManager : MonoBehaviour
    {
        public static KillHouseManager Instance;

        // Unity variables
        public KillHouseStage[] Stages;

        // The doors for the start room
        public KillHouseDoor StartRoomEntranceDoor;
        public KillHouseDoor StartRoomExitDoor;
        public GameObject StartButton;
        public GameObject EndButton;

        // Audio clip for countdown
        public float CountdownLength;

        [Header("Scoring")]
        // Scoring curve
        public KillHouseScoreboard Scoreboard;

        public AnimationCurve ScoreCurve;
        public int TargetScore;
        public int AntiTargetScore;

        private int _currentStage;
        private List<KillHouseTarget> _stageTargets;
        private float _stageTimer = 0f;
        private float _timer = 0f;

        // Keep a record of what happened so we can update the score breakdown later
        private StageBreakdown[] _breakdown;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _stageTargets = new List<KillHouseTarget>();
            // Reset all the stages
            foreach (var stage in Stages)
            {
                foreach (var target in stage.Targets) target.ResetTarget();
                if (stage.ProgressionDoor) stage.ProgressionDoor.Close();
            }

            // Close the start room exit door and open the start room entrance door
            StartRoomExitDoor.Close();
            StartRoomEntranceDoor.Open();

            // Initialize 
            StartButton.SendMessage("Reset");
            _currentStage = -1;
        }

        private void NextStage()
        {
            // If we're not going into the first stage
            if (_currentStage >= 0)
            {
                // Reset all targets in the current stage
                foreach (var target in Stages[_currentStage].Targets) target.ResetTarget();
                // If the current stage had a progression door, open it
                if (Stages[_currentStage].ProgressionDoor)
                    Stages[_currentStage].ProgressionDoor.Open();

                // Record how long the player took
                _breakdown[_currentStage].TimeTaken = _stageTimer;

                // Calculate the score
                CalculateScore();
            }


            // Advance the stage
            _currentStage++;

            // Check if we're done
            if (Stages.Length == _currentStage)
                return;

            // Make a new entry in the stage breakdown
            _breakdown[_currentStage] = new StageBreakdown {StageNumber = _currentStage + 1, StageName = Stages[_currentStage].StageName};


            // Set all the targets in this stage
            _stageTargets.Clear();
            foreach (var target in Stages[_currentStage].Targets)
            {
                target.SetTarget();
                _stageTargets.Add(target);
            }

            // Reset the timer
            _stageTimer = 0f;
        }

        private void CalculateScore()
        {
            var stage = _breakdown[_currentStage];
            var multiplier = ScoreCurve.Evaluate(stage.TimeTaken / Stages[_currentStage].ZeroScoreTime) * 10f;
            stage.Score = (int) ((200 + stage.TargetsHit * TargetScore + stage.AntiTargetsHit * AntiTargetScore) * multiplier);
        }

        private void StartGame()
        {
            // Open the door and start the first stage
            _timer = 0f;
            _breakdown = new StageBreakdown[Stages.Length];
            StartRoomExitDoor.Open();
            EndButton.SendMessage("Reset");
            NextStage();
        }

        public void StartCountdown()
        {
            if (_currentStage != -1) return;
            StartCoroutine(Countdown());
            StartRoomEntranceDoor.Close();
        }

        private void Update()
        {
            // If we're not currently in a game return
            if (_currentStage < 0) return;

            // Keep track of the time taken
            _stageTimer += Time.deltaTime;
            _timer += Time.deltaTime;

            // If we've exceeded the time limit on the current stage, continue to the next regardless of targets
            if (Stages[_currentStage].TimeLimit > 0 && _stageTimer > Stages[_currentStage].TimeLimit) NextStage();
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(CountdownLength);
            StartGame();
        }

        // This is called by the trigger placed at the end of the map 
        public void EndGame()
        {
            if (_currentStage == -1) return;
            _currentStage = -1;
            Scoreboard.UpdateScoreboard(_breakdown, _timer);
            Start();
        }

        // This is called by the targets when they're hit
        public void TargetHit(KillHouseTarget target)
        {
            // Remove it from the active list
            _stageTargets.Remove(target);
            
            // Count it
            if (target.IsAntiTarget) _breakdown[_currentStage].AntiTargetsHit++;
            else _breakdown[_currentStage].TargetsHit++;
            
            // Advance to the next stage if we have no targets left or all the remaining targets are anti-targets
            if (_stageTargets.Count == 0 || _stageTargets.All(x => x.IsAntiTarget)) NextStage();
        }
    }
}