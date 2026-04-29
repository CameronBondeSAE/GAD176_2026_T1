using System;
using Anthill.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class wanderingChiceknj : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [Range(50, 100)] 
    public float amount;
    
    
    private void FixedUpdate()
    {
     GetComponentInParent<Rigidbody>().AddRelativeTorque(0f,Random.Range(-amount,amount),0f);
     // rb.AddRelativeTorque(0f,Random.Range(-amount,amount),0f);
    }
}
