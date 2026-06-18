using Keegan.FOV;
using System.Collections.Generic;
using UnityEngine;


namespace Keegan
{
    public class TestFovObject : MonoBehaviour, IFovDetectable
    {

        [SerializeField]
        private MeshRenderer _renderer;


        /// <summary>
        /// Sets the mesh render state
        /// </summary>
        /// <param name="detected">True if the render is detected</param>
        public void SetDetected(bool detected)
        {
            if (_renderer != null)
                _renderer.enabled = detected;
        }
    }
}