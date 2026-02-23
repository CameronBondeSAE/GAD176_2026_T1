using System;
using System.Collections;
using System.Collections.Generic;
using Frank;
using UnityEngine;

public class ButtonScript : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //private List<Transform> LightTransforms = new List<Transform>();
    
   // private List<IInteractable> LightList  = new List<IInteractable>();

   // private Collider[] lightcollisions;

   public LightManager LightManagerReference;
   
   IEnumerator DelayCoroutine()
   {
       
       foreach (IInteractable c in LightManagerReference.LightList)
       {
           yield return new WaitForSeconds(1f); 
           c.Interact();
            
       }
   }
   
    public void Start()
    {
        
    }

    public void Interact()
    {
        Toggle();
    }

    public void Toggle()
    {
        
        StartCoroutine(DelayCoroutine());

    }
}
