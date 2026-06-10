using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;

public class FOVDetection : MonoBehaviour
{
    [SerializeField, Tooltip("The directions the raycast will perform in")]
    private List<Vector3> _detectionCastDirections = new List<Vector3>();
    [SerializeField,]
    private Transform _directionTransform;
    #if UNITY_EDITOR
    [SerializeField]
    private bool _drawDebug = true;
    #endif

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
