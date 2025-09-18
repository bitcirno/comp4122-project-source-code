using System;
using UnityEngine;

namespace UIFramework.Managers
{
    public class MiniMapManager : MonoBehaviour
    {
        public Transform followTarget;

        private void Update()
        {
            // // target face north
            var transform1 = transform;
            // var transformRotation = transform1.rotation;
            //
            // var angles = transformRotation.eulerAngles;
            // var x = angles.x;
            // var y = followTarget.rotation.eulerAngles.y;
            // var z = angles.z;
            //
            // transformRotation.eulerAngles = new Vector3(x, y, z);
            // transform1.rotation = transformRotation;

            // move with target
            var transformPosition = transform1.position;
            var targetPosition = followTarget.position;
            var x = targetPosition.x;
            var y = transformPosition.y;
            var z = targetPosition.z;
            transform1.position = new Vector3(x, y, z);
        }
    }
}