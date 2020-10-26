using System;
using System.Linq;
using UnityEngine;

namespace H3VR_Kill_House.MappingComponents
{
    public class KillHouseStage : MonoBehaviour
    {
        [Tooltip("A list of targets in this stage")]
        public GameObject[] TargetObjects;
        [Tooltip("The door to open when the stage is completed (Can be null)")]
        public KillHouseDoor ProgressionDoor;
        [Tooltip("The time limit for this stage (0 for none)")]
        public float TimeLimit = 0f;

        public KillHouseTarget[] Targets;
        
        private void Awake()
        {
            Targets = TargetObjects.Select(x => x.GetComponent<KillHouseTarget>()).ToArray();
        }
    }
}