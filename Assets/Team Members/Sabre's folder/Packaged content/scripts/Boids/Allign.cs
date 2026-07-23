using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class Align : MonoBehaviour
    {
        // Variable pointing to your Neighbours component
        public Neighbors neighbors;
        public Rigidbody rb;
        public float force = 100f;
        public Vector3 cross;


        void FixedUpdate()
        {
            // Some are Torque, some are Force		
            Vector3 targetDirection = CalculateMove(neighbors.neighborsList);
            
            // Cross will take YOUR direction and the TARGET direction and turn it into a rotation force vector. It CROSSES through both at 90 degrees
            cross = Vector3.Cross(transform.forward, targetDirection);

            Debug.DrawRay(transform.position, targetDirection * 20f, Color.green);

            rb.AddTorque(cross * force);
        }

        public Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 alignmentDirection = Vector3.zero;

            // Average of all neighbours directions
            // I’m using a list of transforms in my neighbours script, you might be using GameObjects etc
            foreach (Transform item in neighbours)
            {
                alignmentDirection += item.transform.forward;
            }

            alignmentDirection/= neighbours.Count;



    // TODO: Draw a debug line for the DESIRED direction (Your character won’t immediately be snapping to this, line, they’re GRADUALLY turn to it)

            return alignmentDirection;
        }
    }
}


