
using System.Collections;
using UnityEngine;
using Frank;



public class AutomaticDoor : MonoBehaviour, ISwitchable
{
    public void Activate(bool poweredState)
    {
         gameObject.GetComponent<MeshRenderer>().enabled = !poweredState;
         gameObject.GetComponent<BoxCollider>().isTrigger = poweredState;
    }
    
}


