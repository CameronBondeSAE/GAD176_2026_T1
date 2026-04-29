using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Sabre.AI
{
public class Inventory : MonoBehaviour
{
    public BaseCollectable heldCollectable;
    [SerializeField] private Vector3 HeldPosition = new Vector3(0,0, 1f);
    [SerializeField] private float dropHeight = 0.3f;
    public List<BaseCollectable> nearCollectables = new List<BaseCollectable>();
    public CollectableType heldType = CollectableType.None;
    public bool PickUpOverride;
    
    private void OnTriggerEnter(Collider collider)
    {
        BaseCollectable collidedObject = collider.gameObject.GetComponent<BaseCollectable>();
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
        BaseCollectable collidedObject = collider.gameObject.GetComponent<BaseCollectable>();
        
        if(collidedObject != null)
        {
            nearCollectables.Remove(collidedObject);
        }
        
        
    }

    public bool AttemptPickup(BaseCollectable collidedObject)   // bool checks if the pickup was successful
    {
        Debug.Log("Attempting to interact with " + collidedObject);
        if(PickUpOverride == true)
        {
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
        Debug.Log("picking up collectable");
        heldCollectable.PickUp(this);
        heldCollectable.transform.position = transform.position + HeldPosition;
        heldCollectable.transform.SetParent(this.gameObject.transform);
        heldType = heldCollectable.ObjectType;
    }

    public void DropObject()
    {
        Debug.Log("Dropping the " + heldType);
        heldCollectable.Drop();
        Vector3 currentPos = heldCollectable.transform.position;
        heldCollectable.transform.position = new Vector3(currentPos.x, dropHeight, currentPos.z);
        heldCollectable.transform.SetParent(null);
        heldType = CollectableType.None;
        heldCollectable = null;
    }

}
}