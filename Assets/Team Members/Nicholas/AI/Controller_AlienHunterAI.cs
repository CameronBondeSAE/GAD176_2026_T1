using Anthill.AI;
using Unity.Netcode;
using UnityEngine;

namespace Nicholas.AI
{
    /// <summary>
    /// Server-authoritative controller for the Alien AI.
    /// Acts as the middleman between Ant AI and the Alien Models.
    /// The server controls all AI movement and clients receive
    /// the resulting transform through NetworkTransform.
    /// </summary>
    public class Controller_AlienHunterAI : NetworkBehaviour
    {
        [Header("Alien Models")] [SerializeField]
        private Model_AlienHunterAI alienModel;

        [SerializeField] private Model_AlienHunterPatrol patrolModel;
        [SerializeField] private Model_AlienHunterMovement movementModel;

        [Header("Target Settings")] [SerializeField]
        private float lostPlayerDelay = 1.0f;

        [Header("Network Debug")] [SerializeField]
        private bool showNetworkDebug = true;

        [SerializeField] private float debugInterval = 2.0f;

        [SerializeField] private Controller_AlienKillbox killbox;

        private float lostPlayerTimer;
        private bool waitingToLosePlayer;

        private float debugTimer;

        public Model_AlienHunterAI AlienModel => alienModel;
        public Model_AlienHunterPatrol PatrolModel => patrolModel;
        public Model_AlienHunterMovement MovementModel => movementModel;

        private void Awake()
        {
            FindReferences();
        }

        /// <summary>
        /// Finds the Alien Models on the root/parent object.
        /// </summary>
        private void FindReferences()
        {
            if (alienModel == null)
            {
                alienModel = GetComponentInParent<Model_AlienHunterAI>();
            }

            if (patrolModel == null)
            {
                patrolModel = GetComponentInParent<Model_AlienHunterPatrol>();
            }

            if (movementModel == null)
            {
                movementModel = GetComponentInParent<Model_AlienHunterMovement>();
            }

            if (killbox == null)
            {
                killbox = transform.root.GetComponentInChildren<Controller_AlienKillbox>(true);
            }

            Debug.Assert(alienModel != null, "Model_AlienHunterAI is missing from the Alien.");

            Debug.Assert(patrolModel != null, "Model_AlienHunterPatrol is missing from the Alien.");

            Debug.Assert(movementModel != null, "Model_AlienHunterMovement is missing from the Alien.");
        }

        /// <summary>
        /// Configures the Alien depending on whether this instance
        /// is the authoritative server or a remote client.
        /// </summary>
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (killbox != null)
            {
                killbox.PlayerKilled += OnPlayerKilled;
            }

            if (IsServer)
            {
                EnableServerAI();
                return;
            }

            DisableClientAI();
        }

        public override void OnNetworkDespawn()
        {
            if (killbox != null)
            {
                killbox.PlayerKilled -= OnPlayerKilled;
            }

            base.OnNetworkDespawn();
        }

        private void Update()
        {
            if (showNetworkDebug)
            {
                UpdateNetworkDebug();
            }

            if (!IsServer)
            {
                return;
            }

            CheckPlayerTarget();

            UpdateLostPlayerTimer();
        }
        
        /// <summary>
        /// Checks whether the Alien's current player target still exists.
        /// This handles players being destroyed or network despawned.
        /// </summary>
        private void CheckPlayerTarget()
        {
            if (!alienModel.HasPlayerTarget)
            {
                return;
            }

            if (alienModel.CurrentPlayerTarget != null)
            {
                return;
            }

            Debug.Log("Alien player target no longer exists.");

            ResetPlayerTarget();
        }
        
        /// <summary>
        /// Clears all state associated with the previous player target.
        /// </summary>
        private void ResetPlayerTarget()
        {
            movementModel.Stop();

            alienModel.ClearPlayerTarget();

            alienModel.SetTouchingPlayer(false);
            alienModel.SetPlayerKilled(false);

            waitingToLosePlayer = false;
            lostPlayerTimer = 0.0f;
        }

        /// <summary>
        /// Prints the networked Alien position periodically.
        /// Used to compare the server and client copies.
        /// </summary>
        private void UpdateNetworkDebug()
        {
            if (!IsSpawned)
            {
                return;
            }

            debugTimer = debugTimer + Time.deltaTime;

            if (debugTimer < debugInterval)
            {
                return;
            }

            debugTimer = 0.0f;

            Debug.Log("ALIEN NETWORK DEBUG | " + "NetworkObjectId: " + NetworkObjectId + " | IsServer: " + IsServer +
                      " | IsClient: " + IsClient + " | IsOwner: " + IsOwner + " | Root Position: " +
                      transform.root.position);
        }

        /// <summary>
        /// Updates the delay before abandoning a lost player.
        /// Server only.
        /// </summary>
        private void UpdateLostPlayerTimer()
        {
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
        /// Enables the systems responsible for authoritative
        /// Alien movement on the server.
        /// </summary>
        private void EnableServerAI()
        {
            if (movementModel != null)
            {
                movementModel.SetMovementEnabled(true);
            }

            AntAIAgent agent = transform.root.GetComponent<AntAIAgent>();

            if (agent != null)
            {
                agent.enabled = true;
            }

            Debug.Log("Alien server AI enabled.");
        }

        /// <summary>
        /// Prevents remote clients from running their own copy
        /// of the Alien AI or NavMesh movement.
        /// NetworkTransform remains enabled and receives movement
        /// from the server.
        /// </summary>
        private void DisableClientAI()
        {
            AntAIAgent agent = transform.root.GetComponent<AntAIAgent>();

            if (agent != null)
            {
                agent.enabled = false;
            }

            Controller_AlienHunterSense sense = GetComponent<Controller_AlienHunterSense>();

            if (sense != null)
            {
                sense.enabled = false;
            }

            if (movementModel != null)
            {
                movementModel.SetMovementEnabled(false);
            }

            Debug.Log("Alien client AI disabled. NetworkTransform should control movement.");
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
        /// Called when the NavMeshAgent reaches its patrol target.
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
        /// Finishes the search and allows another patrol.
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

        /// <summary>
        /// Called when the server-side FOV detects a player.
        /// </summary>
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

        /// <summary>
        /// Called when the Alien loses sight of its current player.
        /// </summary>
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
        /// Updates the NavMesh destination so the Alien follows
        /// the moving player.
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

        private void OnPlayerKilled()
        {
            if (!IsServer)
            {
                return;
            }

            PlayerKilled();
        }

        /// <summary>
        /// Resets the Alien after the current player has been killed.
        /// Server only.
        /// </summary>
        public void PlayerKilled()
        {
            if (!IsServer)
            {
                return;
            }

            Debug.Log("Alien killed player.");

            movementModel.Stop();

            alienModel.SetPlayerKilled(true);
            alienModel.SetTouchingPlayer(false);

            alienModel.ClearPlayerTarget();

            waitingToLosePlayer = false;
            lostPlayerTimer = 0.0f;

            alienModel.SetPatrolTargetState(false);
            alienModel.SetAtPatrolTarget(false);
            alienModel.SetSearchComplete(false);

            patrolModel.ClearPatrolTarget();
        }
    }
}