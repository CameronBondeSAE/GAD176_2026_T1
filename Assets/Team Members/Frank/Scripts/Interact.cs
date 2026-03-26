using System;
using UnityEngine;
using UnityEngine.XR;

namespace Frank
{
    public class Interact : MonoBehaviour
    {
        public KeyCode keyCode = KeyCode.Space;
        public KeyCode pickupKey = KeyCode.G;
        public Transform headTransform;
        [SerializeField] private Vector3 Hands = new Vector3(0, 0, 0);
        public bool isHolding = false;
        public Transform HandsTransform;
        public GameObject heldObject;
        public GameObject heldItem;
        

        // Update is called once per frame
        void Update()
        {
            //if (isHolding == true)
           // {
                //heldObject.transform.position = headTransform.position + Hands;
           // }
            
            if (Input.GetKeyDown(keyCode)) // if key pressed is space then do the following
            {
                // Raycast shoot in front of us and check what's there
                Ray ray = new Ray(); // create the ray
                ray.origin = headTransform.position; // accesses the origin property of rays and sets the origin to the transform.positon of the player's head
                ray.direction = headTransform.forward; // the direction of the ray is based on the transform of the player's head but slightly offset
                RaycastHit hitInfo = new RaycastHit(); // RaycastHit has a lot of useful information about the object that collides with a ray
                Physics.Raycast(ray, out hitInfo, 3f); // This raycast method is called using the ray, the hitinfo and using a max distance of 3 units


                // Interact with things
                if (hitInfo.transform != null) // primary check - did I hit something - more specifically is there a transform
                {
                    Debug.Log("What I hit : " + hitInfo.transform.gameObject.name);
                    Debug.Log("    Distance to thing I hit : " + hitInfo.distance);
                    Debug.Log("    Where I hit : " + hitInfo.point);


                    if (hitInfo.transform.GetComponentInParent<Divij.IInteractable>() != null) // if so then get the gameobject and if it has an IInteractable component, then call the interact function. 
                    {
                        hitInfo.transform.GetComponentInParent<Divij.IInteractable>().Interact();
                    }
                    



                    /*
                    // BAD CODE. But... it works!
                    // Why? Because if you have a hundred interactable things!!! The code would be a nightmare
                    // It needs to know about EVERY SINGLE ITEM SPECIFICALLY
                    if (hitInfo.transform.GetComponent<SwitchableLight>() != null)
                    {
                       hitInfo.transform.GetComponent<SwitchableLight>().Toggle();
                    }

                    // STILL BAD. What if I have another hundred to go???
                    // What if another team member wants to add another interactable object?
                    //    THEY NOW NEED TO CHANGE THE PLAYER CODE.
                    if (hitInfo.transform.GetComponent<Door>() != null)
                    {
                       hitInfo.transform.GetComponent<Door>().ToggleDoor();
                    }
                    */
                }
                
            }
            
            else if (Input.GetKeyDown(pickupKey))
            {
                Ray ray = new Ray();
                ray.origin = headTransform.position;
                ray.direction = headTransform.forward;
                RaycastHit hitInfo = new RaycastHit();
                Physics.Raycast(ray, out hitInfo, 3f);

                if (hitInfo.transform != null && isHolding == false)
                {
                    Debug.Log("What I hit : " + hitInfo.transform.gameObject.name);
                    Debug.Log("    Distance to thing I hit : " + hitInfo.distance);
                    Debug.Log("    Where I hit : " + hitInfo.point);
                    
                    if (hitInfo.transform.GetComponentInParent<IHoldable>() != null) // if so then get the gameobject and if it has an IHoldable component, then do the following
                    {
                        hitInfo.transform.GetComponentInParent<IHoldable>().Attach(headTransform);
                        isHolding = true;
                        heldItem = hitInfo.transform.gameObject;
                    }
                }
                
                else if (hitInfo.transform != null && isHolding)
                {
                    if (Input.GetKeyDown(pickupKey))
                    {
                        hitInfo.transform.GetComponentInParent<IHoldable>().Detach();
                        isHolding = false;
                    }
                    
                }
                
            }
            
            else if (Input.GetKeyDown(KeyCode.H) && isHolding)
            {
                heldItem.GetComponent<Plug>().Use();
            }
            
            
        }
    }

}

