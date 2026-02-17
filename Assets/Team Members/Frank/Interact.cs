using System;
using UnityEngine;

namespace Frank
{
    public class Interact : MonoBehaviour
    {
        public KeyCode keyCode = KeyCode.Space;
        public Transform headTransform;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                // Raycast shoot in front of us and check what's there
                Ray ray = new Ray();
                ray.origin = headTransform.position;
                ray.direction = headTransform.forward;
                RaycastHit hitInfo = new RaycastHit();
                Physics.Raycast(ray, out hitInfo, 3f);


                // Interact with things
                if (hitInfo.transform != null)
                {
                    Debug.Log("What I hit : " + hitInfo.transform.gameObject.name);
                    Debug.Log("    Distance to thing I hit : " + hitInfo.distance);
                    Debug.Log("    Where I hit : " + hitInfo.point);


                    if (hitInfo.transform.GetComponentInParent<IInteractable>() != null)
                    {
                        hitInfo.transform.GetComponentInParent<IInteractable>().Interact();
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

}

