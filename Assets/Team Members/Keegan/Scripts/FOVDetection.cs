using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;
using Shapes;
using System;
using UnityEngine.Events;

namespace Keegan.FOV
{
    [ExecuteAlways]
    public class FOVDetection : ImmediateModeShapeDrawer
    {
        public enum VisualFOV
        {
            Polyline,
            Polygon,
            None,
        }


        // The directions the raycast will perform in
        [SerializeField, Tooltip("The directions the raycast will perform in")]
        private List<Vector3> _detectionCastDirections = new List<Vector3>();
        // The layer to detect FOV objects on
        [SerializeField, Tooltip("The layers to detect FOV on")]
        private LayerMask _detectionMask;
        [SerializeField, Tooltip("The visual type of the FOV")]
        private VisualFOV _visualType;
        
        // List of all the enemies seen last frame
        private List<IFovDetectable> _enemiesSeenLastFrame = new List<IFovDetectable>();

        // Triggered when the enemy has been seen
        public UnityEvent<IFovDetectable> seenEnemy;
        // Triggered when this has lost sight of the enemy
        public UnityEvent<IFovDetectable> lostEnemy;

        
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
                        seenEnemy?.Invoke(detectable);
                        
                    }
                }
            }

            foreach(var lastFrameDetectable in _enemiesSeenLastFrame)
            {
                if (!detectedThisFrame.Contains(lastFrameDetectable))
                {
                    lastFrameDetectable.SetDetected(false);
                    lostEnemy?.Invoke(lastFrameDetectable);
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
                // Check that we want to to render the visual
                if (_visualType == VisualFOV.Polygon)
                    DrawFovPolygon();
            }
        }

        private void DrawFovPolygon()
        {
            // Define the polygon base information
            Draw.LineGeometry = LineGeometry.Volumetric3D;
            Draw.Matrix = transform.localToWorldMatrix;
            Draw.UseGradientFill = true;
            Draw.GradientFill = GradientFill.Linear(Vector3.zero, Vector3.one * 10f, Color.green, Color.blue, FillSpace.World);
            Draw.Rotation = Quaternion.Euler(90f, 0f, 0f);

            // Get the last to directions
            Vector3 arcDirectionLeft = GetFurthestLeft();
            Vector3 arcDirectionRight = GetFurtherestRight();

            // Get the path points
            Vector2 pathPointA = Vector3.zero;
            Vector2 pathPointB = new Vector3(arcDirectionLeft.x, arcDirectionLeft.z, arcDirectionLeft.z);
            Vector2 pathPointC = new Vector3(arcDirectionRight.x, arcDirectionRight.z, arcDirectionLeft.z);

            // Add Points & draw polygon
            using (var p = new PolygonPath())
            {
                p.AddPoints(pathPointA, pathPointB, pathPointC);
                Draw.Polygon(p, Color.yellow);
            }
        }

        /// <summary>
        /// Gets the end line trace on the left
        /// </summary>
        /// <returns>The direction (Vector3) of the raycast that is further left</returns>
        public Vector3 GetFurthestLeft()
        {
            if(_detectionCastDirections != null && _detectionCastDirections.Count > 0)
            {
                Vector3 furthest = _detectionCastDirections[0];
                foreach(var direction in _detectionCastDirections)
                {
                    if(direction.x > furthest.x)
                    {
                        furthest = direction;
                    }
                }

                return furthest;
            }

            return Vector2.zero;
        }


        /// <summary>
        /// Gets the end raycast on the right
        /// </summary>
        /// <returns>The direction (Vector3) of the raycast that is further right</returns>
        public Vector3 GetFurtherestRight()
        {
            if (_detectionCastDirections != null && _detectionCastDirections.Count > 0)
            {
                Vector3 furthest = _detectionCastDirections[0];
                foreach(var direction in _detectionCastDirections)
                {
                    if (direction.x < furthest.x)
                        furthest = direction;
                }

                return furthest;
            }

            return Vector3.zero;
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
