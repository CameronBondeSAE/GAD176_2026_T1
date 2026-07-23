using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidRef;
        public float speed;
        void Update()
        {
            rigidRef.AddTorque(0f, speed, 0f);
        }
    }
}