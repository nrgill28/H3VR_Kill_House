using FistVR;
using UnityEngine;

namespace H3VR_Kill_House.RuntimeComponents
{
    [AddComponentMenu("")]
    public class PokeButtonReal : FVRInteractiveObject
    {
        public Color UnpushedColor;
        public Color PushedColor;
        private Renderer _renderer;
        private AudioSource _audio;
        private bool _pressed;
        public GameObject Target;
        public string Method;

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<Renderer>();
            _audio = GetComponent<AudioSource>();
        }

        public override void Poke(FVRViveHand hand)
        {
            base.Poke(hand);
            if (_pressed) return;
            _pressed = true;
            Press();
        }

        private void Press()
        {
            _renderer.material.color = PushedColor;
            if (Target != null)
                Target.SendMessage(Method);
            _audio.PlayOneShot(_audio.clip, 0.5f);
        }

        private void Reset()
        {
            _renderer.material.color = UnpushedColor;
            _pressed = false;
        }
    }
}