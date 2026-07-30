using UnityEngine;

namespace Sabre.AI
{

    public class Avoid : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidRef;
        [SerializeField] private float maxDistance = 3f;
        [SerializeField] private float rotationSpeed;

        [SerializeField] private AnimationCurve curve;
        public float curveEval;
        private void FixedUpdate()
        {
            RaycastHit hitInfo;

            if(Physics.Raycast(transform.position, transform.forward, out hitInfo, maxDistance))
            {
                //Debug.DrawLine(transform.position, hitInfo.point, Color.red);
                
                curveEval = curve.Evaluate(Mathf.Clamp01(hitInfo.distance / maxDistance));
                Rotation();
            }

            else
            {
                curveEval = 1f;
                Debug.DrawLine(transform.position, transform.position + transform.forward * maxDistance, Color.blue);
            }
        }

        public void Rotation()
        {

            if(rigidRef != null)
            {
                rigidRef.AddTorque(0,rotationSpeed * (1f - curveEval) ,0, ForceMode.Impulse);
            }

        }
    }
}