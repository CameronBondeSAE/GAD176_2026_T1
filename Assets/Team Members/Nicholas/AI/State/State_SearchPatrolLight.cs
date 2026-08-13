using Anthill.AI;
using Nicholas.AI;
using UnityEngine;

namespace Nicholas.AI.State
{
    /// <summary>
    /// Performs a full 360 degree search at a Light patrol point.
    /// </summary>
    public class State_SearchPatrolLight : AntAIState
    {
        private Controller_AlienHunterAI controller;

        private float totalRotation;

        private const float FullRotation = 360.0f;
        private const float RotationSpeed = 120.0f;

        public override void Create(GameObject gameObject)
        {
            controller = gameObject.GetComponentInChildren<Controller_AlienHunterAI>(true);

            Debug.Assert(controller != null,
                "State_SearchPatrolLight could not find Controller_AlienAI in the Alien hierarchy.");
        }

        public override void Enter()
        {
            Debug.Log("ENTER SEARCH PATROL LIGHT");

            if (!controller.IsServer)
            {
                Finish();
                return;
            }

            controller.MovementModel.SetAutomaticRotation(false);

            totalRotation = 0.0f;

            controller.BeginSearch();
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            //Debug.Log($"SEARCHING: {totalRotation}");

            if (!controller.IsServer)
            {
                Finish();
                return;
            }

            // FOV found someone while searching.
            if (controller.AlienModel.CanSeePlayer)
            {
                controller.MovementModel.SetAutomaticRotation(true);

                Finish();
                return;
            }

            float rotationAmount = RotationSpeed * deltaTime;

            controller.MovementModel.Rotate(rotationAmount);

            totalRotation = totalRotation + rotationAmount;

            if (totalRotation >= FullRotation)
            {
                controller.CompleteSearch();

                Finish();
            }
        }

        public override void Exit()
        {
            controller.MovementModel.SetAutomaticRotation(true);
        }
    }
}