using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    [RequireComponent(typeof(Rigidbody))]
    public class AlienNavMotor : MonoBehaviour
    {
        public float turnSpeedDegrees = 360f;
        public float facingTolerance = 8f;
        public float moveForce = 35f;
        public float maxSpeed = 3.5f;
        public float cornerReachDistance = 0.35f;
        public float repathDistance = 0.25f;
        public float pathEndpointSearchDistance = 2f;
        [Min(0f)] public float separationRadius = 1.5f;
        [Min(0f)] public float separationForce = 1.5f;
        public LayerMask separationMask = 1 << 3;
        public int navMeshAreaMask = NavMesh.AllAreas;

        public Rigidbody body;

        private Collider[] separationHits = new Collider[16];
        private NavMeshPath path;
        private Vector3[] pathCorners = new Vector3[0];
        private Vector3 activeDestination;
        private float activeStoppingDistance;
        private int cornerIndex;
        private bool hasActivePath;
        private bool pathFailed;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            path = new NavMeshPath();
        }

        private void FixedUpdate()
        {
            if (!hasActivePath)
            {
                return;
            }

            if (HasArrived(activeStoppingDistance))
            {
                hasActivePath = false;
                return;
            }

            if (!TryGetCurrentCorner(out Vector3 corner))
            {
                pathFailed = true;
                hasActivePath = false;
                return;
            }

            Vector3 direction = corner - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                Vector3 desiredDirection = GetSteeringDirection(direction);
                FaceDirection(desiredDirection, Time.fixedDeltaTime);
                ApplyForwardForce(desiredDirection);
            }
        }

        public bool PathFailed()
        {
            return pathFailed;
        }

        /// <summary>
        /// Starts moving toward a world position and keeps at least the requested stopping distance.
        /// </summary>
        public bool MoveTo(Vector3 destination, float stoppingDistance)
        {
            bool startSampleSucceeded = TrySamplePathPoint(transform.position, out Vector3 pathStart);
            bool destinationSampleSucceeded = TrySamplePathPoint(destination, out Vector3 pathDestination);

            if (!startSampleSucceeded)
            {
                pathFailed = true;
                hasActivePath = false;
                return false;
            }

            if (!destinationSampleSucceeded)
            {
                pathFailed = true;
                hasActivePath = false;
                return false;
            }

            bool destinationChanged = !hasActivePath || Vector3.Distance(activeDestination, pathDestination) > repathDistance || !Mathf.Approximately(activeStoppingDistance, stoppingDistance);

            if (!destinationChanged)
            {
                return !pathFailed;
            }

            activeDestination = pathDestination;
            activeStoppingDistance = stoppingDistance;
            pathFailed = false;
            EnsurePath();

            bool foundPath = NavMesh.CalculatePath(pathStart, pathDestination, navMeshAreaMask, path);

            if (!foundPath || path.status != NavMeshPathStatus.PathComplete || path.corners.Length == 0)
            {
                pathFailed = true;
                hasActivePath = false;
                pathCorners = new Vector3[0];
                return false;
            }

            pathCorners = path.corners;
            cornerIndex = 0;
            AdvancePastReachedCorners();
            hasActivePath = true;
            return true;
        }

        public bool HasArrived(float distance)
        {
            float arrivalDistance = Mathf.Max(distance, activeStoppingDistance);
            Vector3 flatOffset = activeDestination - transform.position;
            flatOffset.y = 0f;
            return flatOffset.magnitude <= arrivalDistance;
        }

        public void Stop()
        {
            hasActivePath = false;
            pathCorners = new Vector3[0];
            cornerIndex = 0;

            Vector3 velocity = body.linearVelocity;
            body.linearVelocity = new Vector3(0f, velocity.y, 0f);
        }

        public bool Face(Vector3 worldPosition, float deltaTime)
        {
            Vector3 direction = worldPosition - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
            {
                return true;
            }

            FaceDirection(direction, deltaTime);
            float angleToTarget = Vector3.Angle(transform.forward, direction);
            return angleToTarget <= facingTolerance;
        }

        private void FaceDirection(Vector3 direction, float deltaTime)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            float turnAmount = turnSpeedDegrees * deltaTime;
            body.MoveRotation(Quaternion.RotateTowards(body.rotation, targetRotation, turnAmount));
        }

        private bool TryGetCurrentCorner(out Vector3 corner)
        {
            AdvancePastReachedCorners();

            if (cornerIndex < 0 || cornerIndex >= pathCorners.Length)
            {
                corner = activeDestination;
                return false;
            }

            corner = pathCorners[cornerIndex];
            return true;
        }

        private void AdvancePastReachedCorners()
        {
            while (cornerIndex < pathCorners.Length)
            {
                Vector3 cornerOffset = pathCorners[cornerIndex] - transform.position;
                cornerOffset.y = 0f;

                bool isFinalCorner = cornerIndex == pathCorners.Length - 1;
                float reachDistance = isFinalCorner
                    ? Mathf.Max(activeStoppingDistance, cornerReachDistance)
                    : cornerReachDistance;

                if (cornerOffset.magnitude > reachDistance)
                {
                    break;
                }

                cornerIndex++;
            }
        }

        private void ApplyForwardForce(Vector3 desiredDirection)
        {
            float angleToTarget = Vector3.Angle(transform.forward, desiredDirection);
            float steeringScale = Mathf.InverseLerp(120f, 15f, angleToTarget);
            body.AddRelativeForce(Vector3.forward * (moveForce * steeringScale), ForceMode.Acceleration);

            Vector3 velocity = body.linearVelocity;
            Vector3 flatVelocity = new Vector3(velocity.x, 0f, velocity.z);

            if (flatVelocity.magnitude > maxSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * maxSpeed;
                body.linearVelocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
            }
        }

        private Vector3 GetSteeringDirection(Vector3 pathDirection)
        {
            Vector3 separation = CalculateSeparation();
            Vector3 desiredDirection = pathDirection.normalized + separation * separationForce;

            if (desiredDirection.sqrMagnitude < 0.001f)
            {
                return pathDirection;
            }

            return desiredDirection;
        }

        /// <summary>
        /// Pushes this alien away from nearby aliens.
        /// </summary>
        private Vector3 CalculateSeparation()
        {
            if (separationRadius <= 0f || separationForce <= 0f)
            {
                return Vector3.zero;
            }

            int hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                separationRadius,
                separationHits,
                separationMask,
                QueryTriggerInteraction.Ignore);

            Vector3 separation = Vector3.zero;

            for (int i = 0; i < hitCount; i++)
            {
                Collider hit = separationHits[i];

                if (hit == null)
                {
                    continue;
                }

                AlienNavMotor otherMotor = hit.GetComponentInParent<AlienNavMotor>();

                if (otherMotor == null || otherMotor == this)
                {
                    continue;
                }

                Vector3 closestPoint = hit.ClosestPoint(transform.position);
                Vector3 away = transform.position - closestPoint;
                away.y = 0f;

                if (away.sqrMagnitude < 0.001f)
                {
                    away = transform.position - otherMotor.transform.position;
                    away.y = 0f;
                }

                float distance = away.magnitude;

                if (distance <= 0.001f || distance > separationRadius)
                {
                    continue;
                }

                float strength = 1f - distance / separationRadius;
                separation += away.normalized * strength;
            }

            return separation;
        }

        private bool TrySamplePathPoint(Vector3 position, out Vector3 sampledPosition)
        {
            if (NavMesh.SamplePosition(position, out NavMeshHit hit, pathEndpointSearchDistance, navMeshAreaMask))
            {
                sampledPosition = hit.position;
                return true;
            }

            sampledPosition = position;
            return false;
        }

        private void EnsurePath()
        {
            if (path == null)
            {
                path = new NavMeshPath();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (pathCorners == null || pathCorners.Length == 0)
            {
                return;
            }

            Gizmos.color = Color.cyan;
            for (int i = 0; i < pathCorners.Length - 1; i++)
            {
                Gizmos.DrawLine(pathCorners[i], pathCorners[i + 1]);
            }

            if (cornerIndex >= 0 && cornerIndex < pathCorners.Length)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(pathCorners[cornerIndex], cornerReachDistance);
            }
        }
    }
}
