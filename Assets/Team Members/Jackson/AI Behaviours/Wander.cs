using UnityEngine;

namespace Team_Members.Jackson.AI_Behaviours
{
    public class Wander : AIBase
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private int wanderSpeed = 180;

        private void FixedUpdate()
        {
            float perlin = (Mathf.PerlinNoise1D(Time.time) * 2 - 1) * wanderSpeed;

            rb.AddRelativeTorque(0, perlin, 0);
        }
    }
}
