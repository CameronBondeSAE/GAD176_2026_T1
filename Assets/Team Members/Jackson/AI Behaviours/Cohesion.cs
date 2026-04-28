using System.Collections.Generic;
using Team_Members.Jackson.AI_Management;
using UnityEngine;

namespace Team_Members.Jackson.AI_Behaviours
{
    public class Cohesion : AIBase
    {
        [SerializeField] private NeighboursManager neighbours;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float force = 3f;
        [SerializeField] private float minimumDistance = 2f;

        private void FixedUpdate()
        {
            Vector3 directionToTarget = CalculateDirectionTowards(neighbours.neighboursList);

            rb.AddForce(directionToTarget * force);
        }

        private Vector3 CalculateDirectionTowards(List<Transform> neighboursList)
        {
            if (neighboursList.Count == 0)
            {
                return Vector3.zero;
            }
        
            Vector3 targetDirection = Vector3.zero;

            foreach (Transform item in neighboursList)
            {
                Vector3 directionTowardsTarget = (item.position - transform.position).normalized;

                targetDirection += directionTowardsTarget;

                if (Vector3.Distance(transform.position, item.position) >= minimumDistance)
                {
                    targetDirection = Vector3.zero;
                }
            }
        
            targetDirection /= neighboursList.Count;

            return targetDirection;
        }
    }
}
