using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class moveForward : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public float speed = 100f;

    private void Start()
    {
        
    }

    public void FixedUpdate()
    {
     GetComponentInParent<Rigidbody>().AddRelativeForce(Vector3.forward * speed);    
    }
    
}
