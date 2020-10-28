using System;
using FistVR;
using H3VR_Kill_House.Classes;
using UnityEngine;
using WurstMod.MappingComponents;

namespace H3VR_Kill_House.MappingComponents
{
    public class KillHouseTarget : MonoBehaviour
    {
        [Tooltip("Should the player shoot this target it avoid it?")]
        public bool IsAntiTarget;
        [Tooltip("The number of shots required to clear the target")]
        public int ShotsRequired = 1;
        public MovableObject Rotate;
        private bool _active;
        private int _shotsTaken;
        private AudioSource _audio;

        private void Awake()
        {
            _audio = GetComponent<AudioSource>();
        }

        public void SetTarget()
        {
            _active = true;
            _shotsTaken = 0;
            Rotate.MoveTo(0f);
        }
        
        // This is called by the Kill House Manager to reset the stage
        public void ResetTarget()
        {
            _active = false;
            Rotate.MoveTo(1f);
        }

        public void TargetHit()
        {
            // If we're already hit don't do anything
            if (!_active) return;

            // Increase the shot counter and check if we've been hit enough times
            _shotsTaken++;
            if (_shotsTaken < ShotsRequired) return;
            
            // Else tell the manager we're hit and close
            _active = false;
            KillHouseManager.Instance.TargetHit(this);
            Rotate.MoveTo(1f);
            
            // Play the audio clip too
            if (_audio)
                _audio.PlayOneShot(_audio.clip, 1f);
        }
    }
}