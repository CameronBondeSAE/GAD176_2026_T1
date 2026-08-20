using UnityEngine;

namespace Nicholas.AI
{
    public class Debug_FOVColliderHit : MonoBehaviour
    {
        [SerializeField] private float sightDistance = 70.0f;
        [SerializeField] private LayerMask detectionMask;

        private float debugTimer;

        private void Update()
        {
            debugTimer = debugTimer + Time.deltaTime;

            if (debugTimer < 1.0f)
            {
                return;
            }

            debugTimer = 0.0f;

            CheckRay(transform.forward, "CENTRE");

            CheckRay(Quaternion.Euler(0.0f, -25.0f, 0.0f) * transform.forward, "LEFT");

            CheckRay(Quaternion.Euler(0.0f, 25.0f, 0.0f) * transform.forward, "RIGHT");
        }

        private void CheckRay(Vector3 direction, string rayName)
        {
            RaycastHit hit;

            bool didHit = Physics.Raycast(transform.position, direction, out hit, sightDistance, detectionMask,
                QueryTriggerInteraction.Ignore);

            if (!didHit)
            {
                Debug.Log(rayName + " FOV RAY: Nothing hit.");

                Debug.DrawRay(transform.position, direction * sightDistance, Color.green, 1.0f);

                return;
            }

            Debug.Log(rayName + " FOV RAY HIT: " + hit.collider.gameObject.name + " | Layer: " +
                      LayerMask.LayerToName(hit.collider.gameObject.layer) + " | Distance: " + hit.distance +
                      " | Position: " + hit.point);

            Debug.DrawLine(transform.position, hit.point, Color.red, 1.0f);
        }
    }
}