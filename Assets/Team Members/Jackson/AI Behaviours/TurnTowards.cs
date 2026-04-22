using UnityEngine;

namespace Team_Members.Jackson.AI_Behaviours
{
    public class TurnTowards : AIBase
    {
        [SerializeField] private float turnSpeed = 5f;
        private Transform _targetTransform;
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private Rigidbody rb;

        private void Start()
        {
            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            Vector3 targetDir;

            if (targetPosition != Vector3.zero)
            {
                targetDir = (targetPosition - transform.position).normalized;
            }    
        
            else
            {
                targetDir = (_targetTransform.position - transform.position).normalized;
            }

            float angle = Vector3.SignedAngle(transform.forward, targetDir, transform.up);

            rb.AddRelativeTorque(0, angle * turnSpeed, 0);
        }
    }
}