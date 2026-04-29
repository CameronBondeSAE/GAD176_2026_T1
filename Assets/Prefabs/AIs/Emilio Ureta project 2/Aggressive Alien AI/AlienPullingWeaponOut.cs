using System;
using System.Collections;
using Anthill.AI;
using UnityEngine;
using UnityEngine.UIElements;
public class AlienPullingWeaponOut : MonoBehaviour
{

    public MeshRenderer topgun;
    public bool pulleditout;
    public Collectstates States;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        
    }

    void FixedUpdate()
    {
        GetComponent<MeshRenderer>().enabled = topgun.enabled;
        {
            pulleditout = true;
        }

        if (pulleditout)
        {
            StartCoroutine(ShootingGun());
        }
        
    }

    private IEnumerator ShootingGun()
    {
        yield return new WaitForSeconds(2f);
        {
            GetComponentInParent<Collectstates>().isweapondDrawn = true;  
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
