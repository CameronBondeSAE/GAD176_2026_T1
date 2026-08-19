using Keegan.FOV;
using Unity.Netcode;
using UnityEngine;

namespace Nicholas.AI
{
    /// <summary>
    /// Handles server-authoritative FOV detection for the Alien.
    /// Passes detected players to Controller_AlienAI.
    /// </summary>
    public class Controller_AlienFOV : NetworkBehaviour
    {
        [SerializeField] private FOVDetection fovDetection;

        [SerializeField] private Controller_AlienHunterAI alienController;

        private void Awake()
        {
            if (fovDetection == null)
            {
                fovDetection = transform.root.GetComponentInChildren<FOVDetection>(true);
            }

            if (alienController == null)
            {
                alienController = GetComponent<Controller_AlienHunterAI>();
            }
        }

        public override void OnNetworkSpawn()
        {
            if (fovDetection == null)
            {
                Debug.LogError("Controller_AlienFOV could not find FOVDetection.");
                return;
            }

            if (!IsServer)
            {
                return;
            }

            fovDetection.seenEnemy.AddListener(OnTargetSeen);
            fovDetection.lostEnemy.AddListener(OnTargetLost);
        }

        public override void OnNetworkDespawn()
        {
            if (fovDetection == null)
            {
                return;
            }

            fovDetection.seenEnemy.RemoveListener(OnTargetSeen);
            fovDetection.lostEnemy.RemoveListener(OnTargetLost);
        }

        /// <summary>
        /// Called when the Alien FOV sees an IFovDetectable object.
        /// </summary>
        private void OnTargetSeen(IFovDetectable detectable)
        {
            if (!IsServer)
            {
                return;
            }

            Debug.Log("FOV event fired. Detected: " + detectable);

            Player_Controller playerController = detectable as Player_Controller;

            if (playerController == null)
            {
                Debug.LogWarning("Detected IFovDetectable was not Player_Controller.");

                return;
            }

            //Debug.Log("Alien detected player: " + playerController.gameObject.name);

            alienController.PlayerSeen(playerController.transform.root);
        }

        /// <summary>
        /// Called when the Alien FOV loses an IFovDetectable object.
        /// </summary>
        private void OnTargetLost(IFovDetectable detectable)
        {
            if (!IsServer)
            {
                return;
            }

            //Debug.Log("FOV lost event fired. Lost: " + detectable);

            Player_Controller playerController = detectable as Player_Controller;

            if (playerController == null)
            {
                return;
            }

            alienController.PlayerLost(playerController.transform.root);
        }
    }
}