using UnityEngine;

public class Avoid : AIBase
{
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float turnSpeed = 3f;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float curveEval;
    [SerializeField] private LayerMask enemiesAndObstaclesMask;

    private void FixedUpdate()
    {
        bool didItHitAnything = Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, maxDistance, enemiesAndObstaclesMask);

        if (didItHitAnything)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            curveEval = curve.Evaluate(hitInfo.distance / maxDistance);

            if (rb != null)
            {
                curve.Evaluate(hitInfo.distance);
                rb.AddRelativeTorque(0, (turnSpeed / hitInfo.distance), 0, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.green);
        }
    }
}
