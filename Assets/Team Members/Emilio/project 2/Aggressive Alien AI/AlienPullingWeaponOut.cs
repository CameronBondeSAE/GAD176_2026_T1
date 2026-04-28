using System;
using Anthill.AI;
using UnityEngine;
using UnityEngine.UIElements;
public class AlienPullingWeaponOut : MonoBehaviour
{

    public MeshRenderer topgun;

    public Collectstates States;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        
    }

    void FixedUpdate()
    {
        GetComponent<MeshRenderer>().enabled = topgun.enabled;
        {
            States.isCanSeeplayer = true;
            States.isweapondDrawn = true;
        }
        }
        
        // GetComponentInParent<Collectstates>().isCanSeeplayer = true;
        // GetComponentInParent<Collectstates>().isweapondDrawn = true;
        //
        // {
        //     States.isCanSeeplayer = true;
        //     States.isweapondDrawn = true;
        // }
    }
