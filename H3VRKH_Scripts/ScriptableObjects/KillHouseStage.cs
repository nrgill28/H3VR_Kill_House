using H3VR_Kill_House.MappingComponents;
using UnityEngine;

namespace H3VR_Kill_House.ScriptableObjects
{
    [CreateAssetMenu]
    public class KillHouseStage : ScriptableObject
    {
        [Tooltip("A list of targets in this stage")]
        public KillHouseTarget[] Targets;
        [Tooltip("The door to open when the stage is completed (Can be null)")]
        public KillHouseDoor ProgressionDoor;
        [Tooltip("The time limit for this stage (0 for none)")]
        public float TimeLimit = 0f;
    }
}