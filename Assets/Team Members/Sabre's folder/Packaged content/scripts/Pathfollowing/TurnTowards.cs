using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sabre.AI
{
    public class TurnTowards : MonoBehaviour
    {
        //[SerializeField] private Transform target;
        [SerializeField] private Vector3 targetObjectTransform;
        [SerializeField] private Rigidbody rigidRef;
        [SerializeField] private float rotationSpeed = 1;

        public void TrackAngle()
        {
           
            targetObjectTransform = this.gameObject.GetComponent<Pathfind>().targetNode;
            Vector3 Ytarget = new Vector3(targetObjectTransform.x, transform.position.y, targetObjectTransform.z);  // cancels Y axis
            Vector3 NtargetDir  = (Ytarget - transform.position).normalized;    // normalises and finds direction to target

            float angle = Vector3.SignedAngle(transform.forward, NtargetDir , transform.up);
            //Debug.Log("Finding angle to" + targetObjectTransform + " with and angle of" + angle);
            Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);

            Rotation(angle);
        }

        private void Rotation(float angle)
        {
            float normAngle = (Mathf.Clamp01(angle) * 2) - 1;

            if(rigidRef != null)
            {
                rigidRef.AddTorque(0, rotationSpeed * normAngle, 0, ForceMode.Impulse);
            }
        }
    }
}