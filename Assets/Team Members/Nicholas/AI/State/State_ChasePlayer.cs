using Anthill.AI;
using Nicholas.AI;
using UnityEngine;

namespace Nicholas.AI.State
{
    /// <summary>
    /// Chases the currently detected player using the NavMesh.
    /// The server is responsible for all chase decisions.
    /// </summary>
    public class State_ChasePlayer : AntAIState
    {
        private Controller_AlienHunterAI controller;

        public override void Create(GameObject gameObject)
        {
            controller = gameObject.GetComponentInChildren<Controller_AlienHunterAI>(true);

            Debug.Assert(controller != null, "State_ChasePlayer could not find Controller_AlienAI.");
        }

        public override void Enter()
        {
            Debug.Log("ENTER CHASE PLAYER");

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

            bool chaseStarted = controller.BeginChase();

            if (!chaseStarted)
            {
                Debug.LogWarning("Alien could not begin chasing player.");

                Finish();
            }
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            if (!controller.IsServer)
            {
                Finish();
                return;
            }

            if (!controller.AlienModel.HasPlayerTarget)
            {
                controller.StopChase();

                Finish();
                return;
            }

            if (controller.AlienModel.CurrentPlayerTarget == null)
            {
                controller.StopChase();

                Finish();
                return;
            }

            if (controller.AlienModel.IsTouchingPlayer)
            {
                controller.StopChase();

                Finish();
                return;
            }

            controller.UpdateChase();
        }

        public override void Exit()
        {
            if (controller != null && controller.IsServer)
            {
                controller.StopChase();
            }
        }
    }
}