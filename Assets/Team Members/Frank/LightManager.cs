using System.Collections.Generic;
using Frank;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    private List<Transform> LightTransforms = new List<Transform>();
    
    public List<IInteractable> LightList  = new List<IInteractable>();

    private Collider[] lightcollisions;

    public Transform overlapSphereReference;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightcollisions = Physics.OverlapSphere((overlapSphereReference.position), 10f, LayerMask.GetMask("Light"));
        Debug.Log(lightcollisions.Length);
        
        foreach (Collider c in lightcollisions)
        {
            if (LightList.Count < 3)
            {
                LightList.Add(c.GetComponent<IInteractable>());
                //Debug.Log(LightList.Count);
            }

            else
            {
                //Debug.Log(LightList + "is full!");
            }
            
        }

    }
}

//1.157f, 0.488f, 14.578f