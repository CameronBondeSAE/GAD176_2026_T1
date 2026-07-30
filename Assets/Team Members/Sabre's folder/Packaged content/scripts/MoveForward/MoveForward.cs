using UnityEngine;

namespace Sabre.AI
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidRef;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Avoid avoidRef;

        private void FixedUpdate()
        {
            if(rigidRef != null)
            {
                float curveEvalRef = avoidRef.curveEval;

                rigidRef.AddRelativeForce(0,0,moveSpeed * curveEvalRef);
            }
        }
        
    }
}