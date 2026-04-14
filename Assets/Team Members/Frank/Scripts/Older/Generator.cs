using System;
using System.Collections.Generic;
using UnityEngine;
using Frank;
using Unity.VisualScripting;

public class Generator : MonoBehaviour, IInteractable
{
    public Light lightref;
    public Collider[] powerableColliderList;
    private float radius = 10.0f;
    int layerMaskRef = LayerMask.GetMask("Powerable");
    public List<Transform> powerableTransformList;

    public void Toggle()
    {
        lightref.enabled = !lightref.enabled;
    }

    public void FindPowerables()
    {
        if (lightref.enabled == true)
        {
            powerableColliderList = Physics.OverlapSphere(gameObject.transform.position, radius, layerMaskRef);

            foreach (Collider c in powerableColliderList)
            {
                if (powerableTransformList.Count < 5)
                    Debug.Log("hello world");
            }
        }

    }
    
    public void Interact()
    {
        Toggle();
    }

    private void Update()
    {
        FindPowerables();
    }
}
