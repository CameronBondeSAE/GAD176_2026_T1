using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Separation : AIBase
{
    [SerializeField] private NeighboursManager neighbours;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxNeighbourDistance = 6f;
    [SerializeField] private float force = 3f;

    private void FixedUpdate()
    {
        Vector3 directionAway = CalculateDirectionAway(neighbours.neighboursList);

        rb.AddForce(directionAway * force);
    }

    private Vector3 CalculateDirectionAway(List<Transform> neighboursList)
    {
        if (neighboursList.Count == 0)
        {
            return Vector3.zero;
        }

        Vector3 targetDirection = Vector3.zero;

        foreach (Transform item in neighboursList)
        {
            Vector3 directionAwayFromTarget = -(item.position - transform.position).normalized;
            
            float directionMultiplier = maxNeighbourDistance - Vector3.Distance(transform.position, item.position);

            targetDirection += (directionAwayFromTarget * directionMultiplier);
        }

        targetDirection /= neighboursList.Count;
        
        return targetDirection;
    }
}
