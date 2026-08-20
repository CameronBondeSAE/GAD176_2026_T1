using System;
using Unity.Netcode;
using UnityEngine;

namespace Nicholas.AI
{
    /// <summary>
    /// Generic enemy contact script.
    /// Removes a player when the enemy touches them.
    /// </summary>
    public class Controller_AlienKillbox : MonoBehaviour
    {
        public event Action PlayerKilled;

        private void OnTriggerEnter(Collider other)
        {
            if (NetworkManager.Singleton != null)
            {
                if (!NetworkManager.Singleton.IsServer)
                {
                    return;
                }
            }

            Player_Controller playerController = other.transform.root.GetComponentInChildren<Player_Controller>(true);

            if (playerController == null)
            {
                return;
            }

            GameObject playerRoot = playerController.transform.root.gameObject;

            NetworkObject playerNetworkObject = playerRoot.GetComponent<NetworkObject>();

            if (playerNetworkObject != null && playerNetworkObject.IsSpawned)
            {
                Debug.Log(gameObject.name + " despawned player: " + playerRoot.name);

                playerNetworkObject.Despawn(true);

                return;
            }

            Debug.Log(gameObject.name + " destroyed non-networked player: " + playerRoot.name);
                
            PlayerKilled?.Invoke();

            Destroy(playerRoot);
        }
    }
}