using H3VR_Kill_House.Classes;
using UnityEngine;

namespace H3VR_Kill_House.MappingComponents
{
    [RequireComponent(typeof(AudioSource))]
    public class KillHouseTarget : MonoBehaviour
    {
        public float Points;
        private RotatableObject _rotate;
        private AudioSource _audioSource;
        private bool _active;

        private void Awake()
        {
            _rotate = GetComponent<RotatableObject>();
            _audioSource = GetComponent<AudioSource>();
        }
        
        // This is called by the WurstMod Target when the target is hit
        public void Hit()
        {
            // If we're already hit don't do anything
            if (!_active) return;
            _active = false;
            
            // Else tell the manager we're hit and close
            KillHouseManager.Instance.TargetHit(this);
            _rotate.RotateTo(1f);
            
            // If we have an audio source, play it.
            if (_audioSource) _audioSource.Play();
        }

        public void SetTarget()
        {
            _active = true;
            _rotate.RotateTo(0f);
        }
        
        // This is called by the Kill House Manager to reset the stage
        public void ResetTarget()
        {
            _active = false;
            _rotate.RotateTo(1f);
        }
    }
}