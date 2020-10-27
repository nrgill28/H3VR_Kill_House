using FistVR;
using H3VR_Kill_House.Classes;
using UnityEngine;
using WurstMod.MappingComponents;

namespace H3VR_Kill_House.MappingComponents
{
    public class KillHouseTarget : MonoBehaviour
    {
        public float Points;
        public MovableObject Rotate;
        private bool _active;

        public void SetTarget()
        {
            Debug.Log("Target set!");
            _active = true;
            Rotate.MoveTo(0f);
        }
        
        // This is called by the Kill House Manager to reset the stage
        public void ResetTarget()
        {
            Debug.Log("Target reset!");
            _active = false;
            Rotate.MoveTo(1f);
        }

        public void TargetHit()
        {
            Debug.Log("Target hit! (active: " + _active + ")");
            
            // If we're already hit don't do anything
            if (!_active) return;
            _active = false;
            
            // Else tell the manager we're hit and close
            KillHouseManager.Instance.TargetHit(this);
            Rotate.MoveTo(1f);
        }
    }
}