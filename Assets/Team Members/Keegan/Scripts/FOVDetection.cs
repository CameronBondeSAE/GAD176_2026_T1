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

        
        #if UNITY_EDITOR
        [SerializeField]
        private bool _drawDebug = true;
        #endif

        private void Update()
        {
            DetectEnemiesInView();
        }

        private void DetectEnemiesInView()
        {
            foreach(var direction in _detectionCastDirections)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.forward + transform.TransformDirection(direction), out hit, 10f, _detectionMask))
                {
                    
                }
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
