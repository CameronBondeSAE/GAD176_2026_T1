using UnityEngine;
using UnityEngine.AI;

namespace Nicholas.AI
{
    public class Model_AlienHunterMovement : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float arrivalDistance = 0.5f;

        public bool HasDestination { get; private set; }

        private void Awake()
        {
            if (navMeshAgent == null)
            {
                navMeshAgent = GetComponent<NavMeshAgent>();
            }
        }

        /// <summary>
        /// Sends the Alien toward a position using the NavMesh.
        /// </summary>
        public bool SetDestination(Vector3 destination)
        {
            if (navMeshAgent == null)
            {
                Debug.LogWarning("Alien has no NavMeshAgent.");
                return false;
            }

            if (!navMeshAgent.isOnNavMesh)
            {
                Debug.LogWarning("Alien NavMeshAgent is not on a NavMesh.");
                return false;
            }

            bool accepted = navMeshAgent.SetDestination(destination);

            HasDestination = accepted;

            return accepted;
        }

        /// <summary>
        /// Stops the Alien and clears its current NavMesh path.
        /// </summary>
        public void Stop()
        {
            if (navMeshAgent == null || !navMeshAgent.isOnNavMesh)
            {
                return;
            }

            navMeshAgent.ResetPath();

            HasDestination = false;
        }

        /// <summary>
        /// Returns true once the Alien has reached its destination.
        /// </summary>
        public bool HasReachedDestination()
        {
            if (navMeshAgent == null || !navMeshAgent.isOnNavMesh)
            {
                return false;
            }

            if (!HasDestination)
            {
                return false;
            }

            if (navMeshAgent.pathPending)
            {
                return false;
            }

            if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance + arrivalDistance)
            {
                return false;
            }

            if (navMeshAgent.velocity.sqrMagnitude > 0.01f)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Allows the Alien to be manually rotated during searching.
        /// </summary>
        public void SetAutomaticRotation(bool enabled)
        {
            if (navMeshAgent == null)
            {
                return;
            }

            navMeshAgent.updateRotation = enabled;
        }

        public void Rotate(float rotationAmount)
        {
            transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        }
    }
}