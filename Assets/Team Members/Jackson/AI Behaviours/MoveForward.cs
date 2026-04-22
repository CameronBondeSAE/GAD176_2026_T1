using UnityEngine;

namespace Team_Members.Jackson.AI_Behaviours
{
    public class MoveForward : AIBase
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float speed = 100;

        private void FixedUpdate()
        {
            rb.AddRelativeForce(0, 0, speed);
        }
    }
}
 