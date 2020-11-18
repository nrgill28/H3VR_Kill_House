using System.Collections;
using UnityEngine;

namespace H3_Shoothouse.Classes
{
    public class MovableObject : MonoBehaviour
    {
        public Vector3 PositionOffset;
        public Vector3 RotationOffset;
        public float Duration = 0.2f;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Vector3 _endPosition;
        private Quaternion _endRotation;
        
        private void Awake()
        {
            _startPosition = transform.localPosition;
            _startRotation = transform.localRotation;
            _endRotation = _startRotation * Quaternion.Euler(RotationOffset);
            _endPosition = _startPosition + PositionOffset;
        }

        public void MoveTo(float t)
        {
            var targetPos = Vector3.Lerp(_startPosition, _endPosition, t);
            var targetRot = Quaternion.Slerp(_startRotation, _endRotation, t);
            StartCoroutine(LerpTransformTo(targetPos, targetRot, Duration));
        }

        // This is a helper function to rotate the target
        private IEnumerator LerpTransformTo(Vector3 pos, Quaternion rot, float t)
        {
            var elapsed = 0f;
            var startPos = transform.localPosition;
            var startRot = transform.localRotation;

            while (elapsed < t)
            {
                elapsed += Time.deltaTime;
                transform.localPosition = Vector3.Lerp(startPos, pos, elapsed / t);
                transform.localRotation = Quaternion.Slerp(startRot, rot, elapsed / t);
                yield return null;
            }

            // Set these at the end just in case they're slightly off
            transform.localPosition = pos;
            transform.localRotation = rot;
        }
    }
}