
using UnityEngine;
using Frank;



public class AutomaticDoor : MonoBehaviour, IInteractableWithState
{

    public void Interact(bool state)
    { 
        gameObject.GetComponent<MeshRenderer>().enabled = !state;
        gameObject.GetComponent<BoxCollider>().isTrigger = state;
    }
}


