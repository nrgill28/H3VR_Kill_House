using System;
using System.Linq;
using UnityEngine;

namespace H3_Shoothouse.MappingComponents
{
    public class ShoothouseStage : MonoBehaviour
    {
        [Tooltip("The name of the stage")]
        public string StageName;
        [Tooltip("A list of targets in this stage")]
        public ShoothouseTarget[] Targets;
        [Tooltip("The door to open when the stage is completed (Can be null)")]
        public ShoothouseDoor ProgressionDoor;
        [Tooltip("The maximum time someone can spend in this stage to receive any score at all")]
        public float ZeroScoreTime;
        [Tooltip("The time limit for this stage (0 for none)")]
        public float TimeLimit = 0f;
    }
}