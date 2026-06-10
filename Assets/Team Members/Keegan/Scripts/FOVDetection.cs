using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class FOVDetection : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _detectionCastDirections = new List<Vector3>();
    #if UNITY_EDITOR
    [SerializeField]
    private bool _drawDebug = true;
    #endif

#if UNITY_EDITOR


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        
        foreach(Vector3 direction in _detectionCastDirections)
        {
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }
    }

#endif
}
