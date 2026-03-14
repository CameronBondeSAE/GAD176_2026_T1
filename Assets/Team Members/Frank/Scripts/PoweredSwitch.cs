using System;
using System.Collections.Generic;
using UnityEngine;
using Divij;
using Frank;
using Unity.VisualScripting;
using IInteractable = Divij.IInteractable;

public class PoweredSwitch : MonoBehaviour, IInteractable, IPowered
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public bool isPowered = false;
    public List<ISwitchable> ISwitchablelist = new List<ISwitchable>();
    public Transform PoweredSwitchTransform;
    public float radius = 3f;
    
    
    public void Interact()
    {
        if (isPowered == true)
        {
            foreach (ISwitchable CurrentSwitchable in ISwitchablelist)
            {
                CurrentSwitchable.Activate();
            }
        }
        
    }

    public void SetPowered(bool powered)
    {
        isPowered = powered;
        findSwitchables();
    }

    public void findSwitchables()
    {
        
        Collider[] switchableArray = Physics.OverlapSphere(PoweredSwitchTransform.position, radius);

        foreach (var switchableCollider in switchableArray)
        {
            ISwitchable IswitchableToAdd = switchableCollider.gameObject.GetComponent<ISwitchable>();
            
            if(IswitchableToAdd != null)
            {
                ISwitchablelist.Add(IswitchableToAdd);
            }
        }
    }
}
