using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Rigidbody player; 
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnTriggerStay(Collider hurtArea)
    {
        if (hurtArea.tag == "Player")
        {
            hurtArea.GetComponent<HealthSys>().OnDmg(1);
        }
        
    }
    //Quick and dirty Damage area thing. Not ideal to use tags or whatever but it works. 

    // Update is called once per frame
    void Update()
    {
        
    }
}
