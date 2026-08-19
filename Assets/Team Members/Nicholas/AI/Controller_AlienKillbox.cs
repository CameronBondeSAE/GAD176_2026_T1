using Unity.Netcode;
using UnityEngine;

namespace Nicholas.AI
{
    /// <summary>
    /// Damages a player when the Alien makes contact with them.
    /// HealthSys handles health reduction, death, and despawning.
    /// </summary>
    public class Controller_AlienKillbox : MonoBehaviour
    {
        [Header("Damage Settings")] [SerializeField]
        private int damageAmount = 100;

        private void OnTriggerEnter(Collider other)
        {
            if (NetworkManager.Singleton != null && !NetworkManager.Singleton.IsServer)
            {
                return;
            }

            Player_Controller playerController = other.transform.root.GetComponentInChildren<Player_Controller>(true);

            if (playerController == null)
            {
                return;
            }

            HealthSys healthSystem = playerController.transform.root.GetComponentInChildren<HealthSys>(true);

            if (healthSystem == null)
            {
                Debug.LogWarning(gameObject.name + " touched a player but no HealthSys was found.");

                return;
            }

            Debug.Log(gameObject.name + " damaged " + playerController.transform.root.name + " for " + damageAmount +
                      " damage.");

            healthSystem.Damage(damageAmount);
        }
    }
}