using Anthill.AI;
using UnityEngine;

namespace Nicholas.AI.State
{
    public class State_KillPlayer : AntAIState
    {
        private Controller_AlienHunterAI controller;

        public override void Create(GameObject gameObject)
        {
            controller =
                gameObject.GetComponentInChildren<Controller_AlienHunterAI>(true);

            Debug.Assert(
                controller != null,
                "State_KillPlayer could not find Controller_AlienHunterAI.");
        }

        public override void Enter()
        {
            if (controller == null)
            {
                Finish();
                return;
            }

            if (!controller.IsServer)
            {
                Finish();
                return;
            }

            Debug.Log("ENTER KILL PLAYER");

            controller.PlayerKilled();

            Finish();
        }

        public override void Execute(float deltaTime, float timeScale)
        {
        }
    }
}