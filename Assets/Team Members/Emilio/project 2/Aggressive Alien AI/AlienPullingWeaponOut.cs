using System;
using Anthill.AI;
using UnityEngine;
using UnityEngine.UIElements;
public class AlienPullingWeaponOut : MonoBehaviour
{
    public GameObject gun;

    public Collectstates States;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        
    }

    void FixedUpdate()
    {
        GetComponent<GameObject>().gameObject.SetActive(true);

        // GetComponentInParent<Collectstates>().isCanSeeplayer = true;
        // GetComponentInParent<Collectstates>().isweapondDrawn = true;
        //
        // gun.SetActive(true);
        // {
        //     States.isCanSeeplayer = true;
        //     States.isweapondDrawn = true;
        // }
    }
}
