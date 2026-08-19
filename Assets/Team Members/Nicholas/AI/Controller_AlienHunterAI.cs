using Anthill.AI;
using Unity.Netcode;
using UnityEngine;

namespace Nicholas.AI
{
    /// <summary>
    /// Server-authoritative controller for the Alien AI.
    /// Acts as the middleman between Ant AI and the Models.
    /// </summary>
    public class Controller_AlienHunterAI : NetworkBehaviour
    {
        [SerializeField] private Model_AlienHunterAI alienModel;
        [SerializeField] private Model_AlienHunterPatrol patrolModel;
        [SerializeField] private Model_AlienHunterMovement movementModel;

        [SerializeField] private float lostPlayerDelay = 1.0f;

        private float lostPlayerTimer;
        private bool waitingToLosePlayer;

        public Model_AlienHunterAI AlienModel => alienModel;
        public Model_AlienHunterPatrol PatrolModel => patrolModel;
        public Model_AlienHunterMovement MovementModel => movementModel;

        private void Awake()
        {
            if (alienModel == null)
            {
                alienModel = GetComponent<Model_AlienHunterAI>();
            }

            if (patrolModel == null)
            {
                patrolModel = GetComponent<Model_AlienHunterPatrol>();
            }

            if (movementModel == null)
            {
                movementModel = GetComponent<Model_AlienHunterMovement>();
            }

            Debug.Assert(alienModel != null, "Model_AlienAI is missing from the Alien.");

            Debug.Assert(patrolModel != null, "Model_AlienPatrol is missing from the Alien.");

            Debug.Assert(movementModel != null, "Model_AlienMovement is missing from the Alien.");
        }

        public override void OnNetworkSpawn()
        {
            if (!IsServer)
            {
                DisableClientAI();
            }
        }

        private void Update()
        {
            if (!IsServer)
            {
                return;
            }

            if (!waitingToLosePlayer)
            {
                return;
            }

            lostPlayerTimer = lostPlayerTimer + Time.deltaTime;

            if (lostPlayerTimer < lostPlayerDelay)
            {
                return;
            }

            Debug.Log("Alien abandoned player target.");

            alienModel.ClearPlayerTarget();

            waitingToLosePlayer = false;
            lostPlayerTimer = 0.0f;
        }

        /// <summary>
        /// Prevents clients from running their own copy of the Alien brain.
        /// </summary>
        private void DisableClientAI()
        {
            AntAIAgent agent = GetComponent<AntAIAgent>();

            if (agent != null)
            {
                agent.enabled = false;
            }

            Controller_AlienHunterSense sense = GetComponent<Controller_AlienHunterSense>();

            if (sense != null)
            {
                sense.enabled = false;
            }
        }

        /// <summary>
        /// Selects a random patrol point and begins travelling toward it.
        /// Server only.
        /// </summary>
        public bool BeginPatrol()
        {
            if (!IsServer)
            {
                return false;
            }

            Transform target = patrolModel.SelectRandomPatrolTarget();

            if (target == null)
            {
                return false;
            }

            alienModel.SetPatrolTargetState(true);
            alienModel.SetAtPatrolTarget(false);
            alienModel.SetSearchComplete(false);

            movementModel.SetAutomaticRotation(true);

            return movementModel.SetDestination(target.position);
        }

        /// <summary>
        /// Called when the NavMeshAgent reaches its patrol Light.
        /// </summary>
        public void ReachedPatrolTarget()
        {
            if (!IsServer)
            {
                return;
            }

            movementModel.Stop();

            alienModel.SetAtPatrolTarget(true);
        }

        /// <summary>
        /// Prepares the Alien to manually rotate during its search.
        /// </summary>
        public void BeginSearch()
        {
            if (!IsServer)
            {
                return;
            }

            movementModel.Stop();
            movementModel.SetAutomaticRotation(false);

            alienModel.SetSearchComplete(false);
        }

        /// <summary>
        /// Finishes the 360 degree search and allows another patrol.
        /// </summary>
        public void CompleteSearch()
        {
            if (!IsServer)
            {
                return;
            }

            movementModel.SetAutomaticRotation(true);

            alienModel.SetSearchComplete(true);
            alienModel.SetAtPatrolTarget(false);
            alienModel.SetPatrolTargetState(false);

            patrolModel.ClearPatrolTarget();
        }

        public void PlayerSeen(Transform player)
        {
            if (!IsServer)
            {
                return;
            }

            if (player == null)
            {
                return;
            }

            waitingToLosePlayer = false;
            lostPlayerTimer = 0.0f;

            alienModel.SetPlayerTarget(player);
            alienModel.SetCanSeePlayer(true);

            Debug.Log("Alien spotted player: " + player.name);
        }

        public void PlayerLost(Transform player)
        {
            if (!IsServer)
            {
                return;
            }

            if (alienModel.CurrentPlayerTarget != player)
            {
                return;
            }

            alienModel.SetCanSeePlayer(false);

            waitingToLosePlayer = true;
            lostPlayerTimer = 0.0f;

            Debug.Log("Alien lost sight of player.");
        }

        /// <summary>
        /// Starts chasing the currently detected player.
        /// Server only.
        /// </summary>
        public bool BeginChase()
        {
            if (!IsServer)
            {
                return false;
            }

            Transform playerTarget = alienModel.CurrentPlayerTarget;

            if (playerTarget == null)
            {
                return false;
            }

            movementModel.SetAutomaticRotation(true);

            return movementModel.SetDestination(playerTarget.position);
        }

        /// <summary>
        /// Updates the NavMesh destination to follow the moving player.
        /// </summary>
        public bool UpdateChase()
        {
            if (!IsServer)
            {
                return false;
            }

            Transform playerTarget = alienModel.CurrentPlayerTarget;

            if (playerTarget == null)
            {
                return false;
            }

            return movementModel.SetDestination(playerTarget.position);
        }

        /// <summary>
        /// Stops the current chase.
        /// </summary>
        public void StopChase()
        {
            if (!IsServer)
            {
                return;
            }

            movementModel.Stop();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Alien collided with: " + collision.gameObject.name);

            if (!IsServer)
            {
                return;
            }

            Player_Controller playerController = collision.gameObject.GetComponentInParent<Player_Controller>();

            if (playerController == null)
            {
                return;
            }

            Debug.Log("Alien touched player: " + playerController.transform.root.name);

            alienModel.SetTouchingPlayer(true);

            GameObject playerRoot = playerController.transform.root.gameObject;

            Destroy(playerRoot);
        }
    }
}