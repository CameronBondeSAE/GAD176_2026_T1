using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;

namespace Keegan.FOV
{
    public class FOVDetection : MonoBehaviour
    {
        [SerializeField, Tooltip("The directions the raycast will perform in")]
        private List<Vector3> _detectionCastDirections = new List<Vector3>();
        [SerializeField, Tooltip("The layers to detect FOV on")]
        private LayerMask _detectionMask;

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
                        // Add the enemy if it doesn't already exist
                        if(!_enemiesSeenLastFrame.Contains(detectable))
                        {
                            // Add to the detect list
                            detectedThisFrame.Add(detectable);
                            // Update the detected flag on the enemy spotted
                            detectable.SetDetected(true);
                        }
                    }
                }
            }

            // Loop through each of the enemies that were detected last frame
            foreach(var lastFrameDetectable in _enemiesSeenLastFrame)
            {
                // If the new derection list doesn't current detectable
                // then update the detected state on the detectable
                if(!detectedThisFrame.Contains(lastFrameDetectable))
                {
                    lastFrameDetectable.SetDetected(false);
                }
            }

            // Assign detected this frame
            _enemiesSeenLastFrame = detectedThisFrame;
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
