using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class Seperation : MonoBehaviour
    {
        public Neighbors neighbors;
        public Rigidbody rb;
        public float seperationForce;
        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 targetDirection = CalculateMove(neighbors.neighborsList);
            targetDirection = targetDirection + Vector3.Cross(transform.forward, targetDirection);
            rb.AddForce( targetDirection * seperationForce, ForceMode.Impulse);
        }

        private Vector3 CalculateMove(List<Transform> neighbours)
        {
            if (neighbours.Count == 0)
            {
                return Vector3.zero;
            }

            Vector3 alignmentDirection = Vector3.zero;

            foreach (Transform item in neighbours)
            {
                float rad = this.gameObject.GetComponent<SphereCollider>().radius;  // maximum distance
                float dist =  Mathf.Clamp01((rad - Vector3.Distance(item.transform.position, transform.position)) / rad); // the 
                alignmentDirection += Vector3.Normalize(item.transform.position - transform.position) * dist;
            }

            alignmentDirection /= neighbours.Count;
            Debug.DrawLine(transform.position, transform.position + alignmentDirection * 10f, Color.black);
            Debug.Log(alignmentDirection);


            return -alignmentDirection;
        }
    }
}