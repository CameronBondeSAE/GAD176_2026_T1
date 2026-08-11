using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AlienNavMotor : MonoBehaviour
    {
        public float turnSpeedDegrees = 360f;
        public float facingTolerance = 8f;
        public NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
        }

        private void Update()
        {
            Vector3 direction = agent.desiredVelocity;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                FaceDirection(direction, Time.deltaTime);
            }
        }

        public bool PathFailed()
        {
            return !agent.pathPending && agent.pathStatus == NavMeshPathStatus.PathInvalid;
        }

        public bool MoveTo(Vector3 destination, float stoppingDistance)
        {
            if (!agent.isOnNavMesh)
            {
                return false;
            }

            agent.isStopped = false;
            agent.stoppingDistance = stoppingDistance;
            return agent.SetDestination(destination);
        }

        public bool HasArrived(float distance)
        {
            bool pathReady = !agent.pathPending && agent.hasPath;
            bool pathComplete = agent.pathStatus == NavMeshPathStatus.PathComplete;
            bool closeEnough = agent.remainingDistance <= Mathf.Max(distance, agent.stoppingDistance);

            return agent.isOnNavMesh && pathReady && pathComplete && closeEnough;
        }

        public void Stop()
        {
            if (!agent.isOnNavMesh)
            {
                return;
            }

            agent.isStopped = true;
            agent.ResetPath();
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnAmount);
        }
    }
}
