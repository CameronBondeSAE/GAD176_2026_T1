using System.Collections.Generic;
using Keegan.FOV;
using UnityEngine;

namespace Keegan
{
    public class SecurityCamController : FOVDetection
    {
        [SerializeField, Tooltip("Reference to the current object being detected and traced")]
        protected IFovDetectable tracingDetectable;
        
        
    }
}