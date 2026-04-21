using System.Collections.Generic;
using UnityEngine;

public class Align : AIBase
{
    [SerializeField] private NeighboursManager neighbours;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float force = 100f;

    private void FixedUpdate()
    {
        // Some are Torque, some are Force
        Vector3 targetDirection = CalculateMove(neighbours.neighboursList);
        
        // Cross takes YOUR direction and the TARGET direction and turns it into a rotation force vector
        Vector3 cross = Vector3.Cross(transform.forward, targetDirection);
        
        rb.AddTorque(cross * force);
    }

    private Vector3 CalculateMove(List<Transform> neighboursList)
    {
        if (neighboursList.Count == 0)
        {
            return Vector3.zero;
        }
        
        Vector3 alignmentDirection = Vector3.zero;
            
        // Average of all neighbours directions
        foreach (Transform item in neighboursList)
        {
            alignmentDirection += item.transform.forward;
        }

        alignmentDirection /= neighboursList.Count;
        
        // Where I WANT to face
        Debug.DrawRay(transform.position, alignmentDirection * 10f, Color.blue);
        
        // Where I'm facing right now 
        Debug.DrawRay(transform.position, transform.forward * 10f, Color.green);

        return alignmentDirection;
    }
}
