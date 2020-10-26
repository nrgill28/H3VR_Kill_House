using System.Collections;
using UnityEngine;

namespace H3VR_Kill_House.Classes
{
    public class MovableObject : MonoBehaviour
    {
        public Transform MoveObject;
        public Vector3 EndRotation;
        public Vector3 EndPosition;
        private Vector3 _startRotation;
        private Vector3 _startPosition;
        public float RotationTime = 0.2f;

        private void Start()
        {
            // When the map loads we want to fold the target down and make it inactive
            _startRotation = MoveObject.localRotation.eulerAngles;
            _startPosition = MoveObject.localPosition;
        }

        public void MoveTo(float t)
        {
            StartCoroutine(SlerpTransformTo(Vector3.Slerp(_startRotation, EndRotation, t), Vector3.Lerp(_startPosition, EndPosition, t), RotationTime));
        }

        // This is a helper function to rotate the target
        private IEnumerator SlerpTransformTo(Vector3 endRot, Vector3 endPos, float t)
        {
            var startRot = MoveObject.localRotation.eulerAngles;
            var startPos = MoveObject.localPosition;
            var time = 0f;
            while (time < t)
            {
                time += Time.deltaTime;
                MoveObject.localRotation = Quaternion.Euler(Vector3.Slerp(startRot, endRot, time));
                MoveObject.localPosition = Vector3.Lerp(startPos, endPos, time);
                yield return null;
            }

            MoveObject.localRotation = Quaternion.Euler(endRot);
            MoveObject.localPosition = endPos;
        }
    }
}