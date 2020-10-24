using System.Collections;
using H3VR_Kill_House.MappingComponents;
using UnityEngine;

namespace H3VR_Kill_House.Classes
{
    public class RotatableObject : MonoBehaviour
    {
        [Tooltip("The angle to rotate to when opened")]
        public Vector3 EndRotation;
        private Vector3 _startRotation;
        public float RotationTime = 0.2f;

        private void Start()
        {
            // When the map loads we want to fold the target down and make it inactive
            _startRotation = transform.localRotation.eulerAngles;
        }

        public void RotateTo(float t)
        {
            StartCoroutine(SlerpRotationTo(Vector3.Slerp(_startRotation, EndRotation, t), RotationTime));
        }

        // This is a helper function to rotate the target
        private IEnumerator SlerpRotationTo(Vector3 endRot, float t)
        {
            var startRot = transform.localRotation.eulerAngles;
            var time = 0f;
            while (time < t)
            {
                time += Time.deltaTime;
                transform.localRotation = Quaternion.Euler(Vector3.Slerp(startRot, endRot, time));
                yield return null;
            }
        }
    }
}