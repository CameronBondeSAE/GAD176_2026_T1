using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;
using Shapes;

namespace Keegan.FOV
{
    [ExecuteAlways]
    public class FOVDetection : ImmediateModeShapeDrawer
    {
        // The directions the raycast will perform in
        [SerializeField, Tooltip("The directions the raycast will perform in")]
        private List<Vector3> _detectionCastDirections = new List<Vector3>();
        // The layer to detect FOV objects on
        [SerializeField, Tooltip("The layers to detect FOV on")]
        private LayerMask _detectionMask;
        
        // List of all the enemies seen last frame
        private List<IFovDetectable> _enemiesSeenLastFrame = new List<IFovDetectable>();

        
        #if UNITY_EDITOR
        [SerializeField]
        private bool _drawDebug = true;
        #endif

        private void Update()
        {
            DetectEnemiesInView();
        }

        /// <summary>
        /// Performs detection for any detectable objects that are within each of the raycast
        /// </summary>
        private void DetectEnemiesInView()
        {
            // Define a list of enemies that are detected this frame
            List<IFovDetectable> detectedThisFrame = new List<IFovDetectable>();

            // Loop through each of the target direction
            foreach(var direction in _detectionCastDirections)
            {
                // Perform the raycast for detection
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward + transform.TransformDirection(direction), out hit, 10f, _detectionMask))
                {
                    // Check if the hit collider has the IFovDetectable interface
                    IFovDetectable detectable = hit.collider.GetComponent<IFovDetectable>();
                    if(detectable != null)
                    {

                        // Add to the detect list
                        detectedThisFrame.Add(detectable);
                        // Update the detected flag on the enemy spotted
                        detectable.SetDetected(true);
                        
                    }
                }
            }

            foreach(var lastFrameDetectable in _enemiesSeenLastFrame)
            {
                if (!detectedThisFrame.Contains(lastFrameDetectable))
                {
                    lastFrameDetectable.SetDetected(false);
                }
            }

            // Clear the list for safety
            _enemiesSeenLastFrame.Clear();
            // Assign the new list
            _enemiesSeenLastFrame = detectedThisFrame;
        }

        public override void DrawShapes(Camera cam)
        {
            base.DrawShapes(cam);

            using(Draw.Command(cam))
            {
                Draw.LineGeometry = LineGeometry.Volumetric3D;
                Draw.Matrix = transform.localToWorldMatrix;


            }
        }
    #if UNITY_EDITOR


        private void OnDrawGizmosSelected()
        {
            if(_drawDebug)
            {
                Gizmos.color = Color.yellow;
                foreach(var direction in _detectionCastDirections)
                {
                    Gizmos.DrawLine(transform.position, transform.position + transform.TransformDirection(direction));
                }
            }
        }

    #endif
    }
}
