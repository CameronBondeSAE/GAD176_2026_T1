using System.Collections.Generic;
using Keegan.FOV;
using UnityEngine;

namespace Keegan
{
    public class SecurityCamController : FOVDetection
    {
        public enum CurrentRotateDirection
        {
            Left,
            Right
        }
        
        
        [SerializeField, Tooltip("Reference to the current object being detected and traced")]
        protected IFovDetectable tracingDetectable;

        [SerializeField, Tooltip("How far left the camera can look")]
        private float maxLookLeft = -75;

        [SerializeField, Tooltip("How far right the camera can look")]
        private float maxLookRight = 75;

        [SerializeField, Tooltip("Reference to the direction the camera is rotating in")]
        private CurrentRotateDirection currentRotateDirection;

        private float currentRotation = 0f;

        [SerializeField, Tooltip("The rate at which the camera rotates")]
        private float _rotationRate;
            
            
        protected override void Update()
        {
            base.Update();


            if (tracingDetectable == null)
            {
                if (_detectedThisFrame.Count > 0)
                    tracingDetectable = _detectedThisFrame[0];
            }
            else
            {
                if (!_detectedThisFrame.Contains(tracingDetectable))
                    tracingDetectable = null;
            }
        }

        private void RotateCamera()
        {
            if (tracingDetectable == null)
            {
                currentRotation += ((currentRotateDirection == CurrentRotateDirection.Left ? -_rotationRate : _rotationRate) * Time.deltaTime);
            }
        }
    }
}