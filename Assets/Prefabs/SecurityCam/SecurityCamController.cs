using System.Collections.Generic;
using Keegan.FOV;
using UnityEngine;

namespace Keegan
{
    public class SecurityCamController : FOVDetection
    {
        [SerializeField, Tooltip("Reference to the current object being detected and traced")]
        protected IFovDetectable tracingDetectable;

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


        private void FollowTarget()
        {
            
        }
    }
}