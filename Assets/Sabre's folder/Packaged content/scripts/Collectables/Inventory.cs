using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CameronBonde;
namespace Sabre.AI
{
public class Inventory : MonoBehaviour
{
    public BaseCollectable heldCollectable;
    [SerializeField] private Vector3 HeldPosition = new Vector3(0,0, 1f);
    [SerializeField] private float dropHeight = -3.3f;
    public List<IInteractable> nearCollectables = new List<IInteractable>();
    public CollectableType heldType = CollectableType.None;
    public bool PickUpOverride;
    public GuardSense charbase;
    
    private void OnTriggerEnter(Collider collider)
    {
        IInteractable collidedObject = collider.gameObject.GetComponent<IInteractable>();
        if(AttemptPickup(collidedObject) == false)
        {
            if(collidedObject != null)
            {
                nearCollectables.Add(collidedObject);
            }
        }
        
    }
    private void OnTriggerExit(Collider collider)
    {
        IInteractable collidedObject = collider.gameObject.GetComponent<IInteractable>();
        
        if(collidedObject != null)
        {
            if(collidedObject == heldCollectable)
            {
                Debug.Log("Leaving Behind held collectable");
                //DropObject();
            }

            nearCollectables.Remove(collidedObject);
        }
        
        
        
    }

    public bool AttemptPickup(IInteractable interactableObject)   // bool checks if the pickup was successful
    {
        if(interactableObject as BaseCollectable)    // ignores all other collectables
        {
            Debug.Log("Attempting to interact with " + interactableObject);
        }
        else
        {
        
            return false;   
        }

        BaseCollectable collidedObject = (BaseCollectable)interactableObject;


        if(PickUpOverride == true)
        {
            Debug.Log("pick up override detected");
            return false;
        }
        
        //Debug.Log("Attempting to pick up collectable " + collidedObject);
        if(collidedObject != null && collidedObject.currentlyHeld != true && heldCollectable == null && collidedObject.overridePickup != true)
        {
            heldCollectable = collidedObject;
            PickUpObject();
            return true;
        }
        return false;
    }

    public void PickUpObject()
    {
        if(charbase.boxStation.BoxSafe == true && heldCollectable.ObjectType == CollectableType.Box)
        {
            Debug.Log("Canceling pickup of safe box");
            return; 
        }

        Debug.Log("picking up collectable");
        heldCollectable.Interact();
        heldType = heldCollectable.ObjectType;

        if(heldCollectable.Owner == null && heldCollectable.overridePickup == false)
        {
            Debug.Log("Pickup error; overriding to be owner of: " + heldCollectable);

            heldCollectable.Owner = charbase.health;
            heldCollectable.transform.position = heldCollectable.transform.position + HeldPosition;
            heldCollectable.transform.SetParent(this.gameObject.transform);
        }
    }

    public void DropObject()
    {
        if(heldCollectable == null)
        {
            Debug.Log("No held collectable to drop");
            return;
        }

        Debug.Log("Dropping the " + heldType);
        heldCollectable.Drop();
        heldType = CollectableType.None;
        heldCollectable = null;
    }

}
}