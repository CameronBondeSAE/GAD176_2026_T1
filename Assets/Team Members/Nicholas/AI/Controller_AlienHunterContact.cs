using UnityEngine;

namespace Nicholas.AI
{
    public class Controller_AlienHunterContact : MonoBehaviour
    {
        private Controller_AlienHunterAI alienController;

        private void Awake()
        {
            alienController = transform.root.GetComponentInChildren<Controller_AlienHunterAI>(true);

            Debug.Assert(alienController != null, "Controller_AlienContact could not find Controller_AlienAI.");
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ALIEN TRIGGER HIT: " + other.gameObject.name);

            if (alienController == null)
            {
                return;
            }

            if (!alienController.IsServer)
            {
                return;
            }

            GameObject possiblePlayerRoot = other.transform.root.gameObject;

            Player_Controller playerController = possiblePlayerRoot.GetComponentInChildren<Player_Controller>(true);

            if (playerController == null)
            {
                return;
            }

            Debug.Log("ALIEN IS TOUCHING PLAYER");

            alienController.AlienModel.SetTouchingPlayer(true);
        }
    }
}