using System;
using UnityEngine;


public class Interact : MonoBehaviour
{
   public KeyCode   keyCode = KeyCode.Space;
   public Transform headTransform;
  
   // Update is called once per frame
   void Update()
   {
      if (Input.GetKeyDown(keyCode))
      {
         // Raycast shoot in front of us and check what's there
         Ray ray = new Ray();
         ray.origin    = headTransform.position;
         ray.direction = headTransform.forward;
         RaycastHit thingInFrontOfMe = new RaycastHit();
         Physics.Raycast(ray, out thingInFrontOfMe, 3f);
        
        
         // Interact with things
         if (thingInFrontOfMe.transform != null)
         {
            Debug.Log("What I hit : " + thingInFrontOfMe.transform.gameObject.name);
            Debug.Log("    Distance to thing I hit : " + thingInFrontOfMe.distance);
            Debug.Log("    Where I hit : " + thingInFrontOfMe.point);


            // Works for EVERYTHING... FOREVER
            if (thingInFrontOfMe.transform.GetComponentInParent<IInteractable>() != null)
            {
	            thingInFrontOfMe.transform.GetComponentInParent<IInteractable>().Interact();
            }

            
            // BAD CODE. But... it works!
            // Why? Because if you have a hundred interactable things!!! The code would be a nightmare
            // It needs to know about EVERY SINGLE ITEM SPECIFICALLY
            // if (thingInFrontOfMe.transform.GetComponent<SwitchableLight>() != null)
            // {
            //    thingInFrontOfMe.transform.GetComponent<SwitchableLight>().Toggle();
            // }
           
            // STILL BAD. What if I have another hundred to go???
            // What if another team member wants to add another interactable object?
            //    THEY NOW NEED TO CHANGE THE PLAYER CODE.
            // if (thingInFrontOfMe.transform.GetComponent<Door>() != null)
            // {
            //    thingInFrontOfMe.transform.GetComponent<Door>().ToggleDoor();
            // }
            
         }
      }
   }


   // You can also check collisions with Unity automatically ran functions
   private void OnCollisionEnter(Collision other)
   {
      Debug.Log(other.gameObject.name);
   }


   private void OnTriggerEnter(Collider other)
   {
      Debug.Log(other.gameObject.name);
   }
}


