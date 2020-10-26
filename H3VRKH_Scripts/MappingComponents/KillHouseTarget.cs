using FistVR;
using H3VR_Kill_House.Classes;
using UnityEngine;
using WurstMod.MappingComponents;

namespace H3VR_Kill_House.MappingComponents
{
    [AddComponentMenu("")]
    public class KillHouseTarget : MonoBehaviour, IFVRDamageable
    {
        public float Points;
        private MovableObject _rotate;
        private bool _active;

        private void Awake()
        {
            _rotate = GetComponent<MovableObject>();
        }

        public void SetTarget()
        {
            _active = true;
            _rotate.MoveTo(0f);
        }
        
        // This is called by the Kill House Manager to reset the stage
        public void ResetTarget()
        {
            _active = false;
            _rotate.MoveTo(1f);
        }

        public void Damage(Damage dam)
        {
            // If the damage isn't caused by a projectile or an explosive, ignore it.
            if (dam.Class != FistVR.Damage.DamageClass.Projectile && dam.Class != FistVR.Damage.DamageClass.Explosive)
                return;
            
            Debug.Log("Target hit! (active: " + _active + ")");
            
            // If we're already hit don't do anything
            if (!_active) return;
            _active = false;
            
            // Else tell the manager we're hit and close
            KillHouseManager.Instance.TargetHit(this);
            _rotate.MoveTo(1f);
        }
    }

    public class KillHouseTargetProxy : ComponentProxy
    {
        public override void InitializeComponent()
        {
            gameObject.AddComponent<KillHouseTarget>();
        }
    }
}