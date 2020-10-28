using System;
using System.Linq;
using UnityEngine;

namespace H3VR_Kill_House.MappingComponents
{
    public class KillHouseStage : MonoBehaviour
    {
        [Tooltip("The name of the stage")]
        public string StageName;
        [Tooltip("A list of targets in this stage")]
        public KillHouseTarget[] Targets;
        [Tooltip("The door to open when the stage is completed (Can be null)")]
        public KillHouseDoor ProgressionDoor;
        [Tooltip("The maximum time someone can spend in this stage to receive any score at all")]
        public float ZeroScoreTime;
        [Tooltip("The time limit for this stage (0 for none)")]
        public float TimeLimit = 0f;
    }
}