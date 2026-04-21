using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Pathfinder : AIBase
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float turnSpeed = 5f;
    [SerializeField] public UnityEvent reachedEndEvent;
    private NavMeshPath _path;
    public Transform targetTransform;
    private int cornerIndex = 1;

    /*private void Start()
    {
        //_targetTransform = GameObject.FindGameObjectWithTag("Target").transform;

        _path = new NavMeshPath();

        if (NavMesh.CalculatePath(transform.position, targetTransform.position, areaMask: Int32.MaxValue, _path))
        {
            Debug.Log("Calculated path successfully");
        }
        else
        {
            Debug.LogError("Could not calculate path");
        }
    }*/
    
    public void CalculatePath()
    {
        //_targetTransform = GameObject.FindGameObjectWithTag("Target").transform;
        
        _path = new NavMeshPath();

        cornerIndex = 1;

        if (NavMesh.CalculatePath(transform.position, targetTransform.position, areaMask: Int32.MaxValue, _path))
        {
            Debug.Log("Calculated path successfully");
        }
        else
        {
            Debug.LogError("Could not calculate path");
        }
    }

    public void ExecutePath()
    {
        if (_path != null && cornerIndex < _path.corners.Length)
        {
            if (Vector3.Distance(transform.position, _path.corners[cornerIndex]) <= minDistance)
            {
                cornerIndex++;
            }
        }
        else
        {
            reachedEndEvent.Invoke();
        }

        Vector3 targetDir = Vector3.zero;

        if (targetTransform != null & cornerIndex < _path.corners.Length)
        {
            targetDir = (_path.corners[cornerIndex] - transform.position).normalized;
        }

        float angle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);

        rb.AddRelativeTorque(0, angle * turnSpeed, 0);
    }

    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            Gizmos.DrawLineStrip(_path.corners, false);

            Gizmos.color = Color.green;
            if (cornerIndex < _path.corners.Length)
            {
                Gizmos.DrawSphere(_path.corners[cornerIndex], 0.5f / 2f);
            }
        }
    }
}
