using System;
using UnityEngine;

namespace Divij
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
                    // Debug.Log("    Distance to thing I hit : " + hitInfo.distance);
                    // Debug.Log("    Where I hit : " + hitInfo.point);


                    if (hitInfo.transform.GetComponentInParent<IInteractable>() != null)
                    {
                        hitInfo.transform.GetComponentInParent<IInteractable>().Interact();
                    }
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