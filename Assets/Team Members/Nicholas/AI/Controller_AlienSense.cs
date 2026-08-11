using Anthill.AI;
using Unity.Netcode;
using UnityEngine;

namespace Nicholas.AI
{
    public class Controller_AlienHunterSense : NetworkBehaviour, ISense
    {
        [SerializeField] private Model_AlienHunterAI alienModel;

        public void CollectConditions(AntAIAgent agent, AntAICondition worldState)
        {
            if (!IsServer)
            {
                return;
            }

            worldState.BeginUpdate(agent.planner);

            worldState.Set(HunterAI.HasPatrolTarget, alienModel.HasPatrolTarget);

            worldState.Set(HunterAI.AtPatrolTarget, alienModel.AtPatrolTarget);

            worldState.Set(HunterAI.SearchComplete, alienModel.SearchComplete);

            worldState.Set(HunterAI.CanSeePlayer, alienModel.CanSeePlayer);

            worldState.Set(HunterAI.HasPlayerTarget, alienModel.HasPlayerTarget);

            worldState.Set(HunterAI.IsTouchingPlayer, alienModel.IsTouchingPlayer);

            worldState.Set(HunterAI.PlayerKilled, alienModel.PlayerKilled);

            worldState.EndUpdate();
        }
    }
}