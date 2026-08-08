using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AlienNavMotor : MonoBehaviour
    {
        [SerializeField] private float turnSpeedDegrees = 360f;
        [SerializeField] private float facingTolerance = 8f;

        private NavMeshAgent _agent;
        public bool PathFailed => !_agent.pathPending && _agent.pathStatus == NavMeshPathStatus.PathInvalid;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
        }

        private void Update()
        {
            Vector3 direction = _agent.desiredVelocity;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f)
                FaceDirection(direction, Time.deltaTime);
        }

        public bool MoveTo(Vector3 destination, float stoppingDistance)
        {
            if (!_agent.isOnNavMesh)
                return false;
            _agent.isStopped = false;
            _agent.stoppingDistance = stoppingDistance;
            return _agent.SetDestination(destination);
        }

        public bool HasArrived(float distance)
        {
            return _agent.isOnNavMesh && !_agent.pathPending && _agent.hasPath &&
                   _agent.pathStatus == NavMeshPathStatus.PathComplete &&
                   _agent.remainingDistance <= Mathf.Max(distance, _agent.stoppingDistance);
        }

        public void Stop()
        {
            if (!_agent.isOnNavMesh)
                return;
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        public bool Face(Vector3 worldPosition, float deltaTime)
        {
            Vector3 direction = worldPosition - transform.position;
            direction.y = 0f;
            if (direction.sqrMagnitude < 0.001f)
                return true;
            FaceDirection(direction, deltaTime);
            return Vector3.Angle(transform.forward, direction) <= facingTolerance;
        }

        private void FaceDirection(Vector3 direction, float deltaTime)
        {
            Quaternion target = Quaternion.LookRotation(direction.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, turnSpeedDegrees * deltaTime);
        }
    }
}
