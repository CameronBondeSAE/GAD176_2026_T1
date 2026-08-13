using Anthill.AI;
using Nicholas.AI;
using UnityEngine;

namespace Nicholas.AI.State
{
    /// <summary>
    /// Default Ant AI action.
    /// Selects a random Light and travels toward it.
    /// </summary>
    public class State_Patrol : AntAIState
    {
        private Controller_AlienHunterAI controller;

        public override void Create(GameObject gameObject)
        {
            controller = gameObject.GetComponentInChildren<Controller_AlienHunterAI>(true);

            Debug.Assert(controller != null, "State_Patrol could not find Controller_AlienAI in the Alien hierarchy.");
        }

        public override void Enter()
        {
            if (!controller.IsServer)
            {
                Finish();
                return;
            }

            bool patrolStarted = controller.BeginPatrol();

            if (!patrolStarted)
            {
                Debug.LogWarning("Alien failed to begin patrol.");

                Finish();
            }
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            if (controller == null)
            {
                Debug.LogError("State_Patrol controller is null.");
                Finish();
                return;
            }

            if (controller.AlienModel == null)
            {
                Debug.LogError("State_Patrol AlienModel is null.");
                Finish();
                return;
            }

            if (controller.MovementModel == null)
            {
                Debug.LogError("State_Patrol MovementModel is null.");
                Finish();
                return;
            }

            if (controller.AlienModel.CanSeePlayer)
            {
                controller.MovementModel.Stop();
                Finish();
                return;
            }

            if (controller.MovementModel.HasReachedDestination())
            {
                controller.ReachedPatrolTarget();
                Finish();
            }
        }

        public override void Exit()
        {
        }
    }
}