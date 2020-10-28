using FistVR;
using H3VR_Kill_House.RuntimeComponents;
using UnityEngine;
using WurstMod.MappingComponents;

namespace H3VR_Kill_House.MappingComponents
{
    [RequireComponent(typeof(AudioSource))]
    public class PokeButton : ComponentProxy
    {
        public Color UnpushedColor;
        public Color PushedColor;
        public GameObject Target;
        public string Method;
        
        public override void InitializeComponent()
        {
            var real = gameObject.AddComponent<PokeButtonReal>();
            real.IsSimpleInteract = true;
            real.EndInteractionDistance = 0.1f;
            real.EndInteractionIfDistant = true;

            real.UnpushedColor = UnpushedColor;
            real.PushedColor = PushedColor;
            real.Target = Target;
            real.Method = Method;
            Destroy(this);
        }
    }
}