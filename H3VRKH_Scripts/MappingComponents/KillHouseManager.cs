using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Audio clip for countdown
        public float CountdownLength;
        
        private int _currentStage;
        private float _pointsTotal;
        private List<KillHouseTarget> _stageTargets;
        private float _stageTimer = 0f;
        private float _timer = 0f;
        
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
                Debug.Log("Resetting stage; " + stage.Targets.Length + " targets");
                foreach (var target in stage.Targets) target.ResetTarget();
                if (stage.ProgressionDoor) stage.ProgressionDoor.Close();
            }
            
            // Close the start room exit door and open the start room entrance door
            StartRoomExitDoor.Close();
            StartRoomEntranceDoor.Open();

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
            }


            // Advance the stage
            _currentStage++;
            
            // Check if we're done
            if (Stages.Length == _currentStage)
            {
                EndGame();
                return;
            }
            
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

        private void StartGame()
        {
            // Open the door and start the first stage
            StartRoomExitDoor.Open();
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
            _timer += Time.deltaTime;
            _stageTimer += Time.deltaTime;
                
            // If we've exceeded the time limit on the current stage, continue to the next regardless of targets
            if (Stages[_currentStage].TimeLimit > 0 && _stageTimer > Stages[_currentStage].TimeLimit) NextStage();
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(CountdownLength);
            StartGame();
        }

        private void EndGame()
        {
            _currentStage = -1;
            // TODO: Display the score somehow
        }
        
        public void TargetHit(KillHouseTarget target)
        {
            _pointsTotal += target.Points;
            _stageTargets.Remove(target);
            if (_stageTargets.Count == 0) NextStage();
        }
    }
}