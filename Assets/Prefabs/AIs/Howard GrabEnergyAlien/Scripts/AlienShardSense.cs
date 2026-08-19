using Anthill.AI;
using UnityEngine;

namespace Howard.ShardAI
{
    public class AlienShardSense : MonoBehaviour, ISense
    {
        public const string ShardAvailable = "Shard Available";
        public const string HasShardTarget = "Has Shard Target";
        public const string NearShard = "Near Shard";
        public const string HoldingShard = "Holding Shard";
        public const string HasDropTarget = "Has Drop Target";
        public const string NearDropTarget = "Near Drop Target";
        public const string ShardDelivered = "Shard Delivered";

        public AlienShardContext context;

        private void Awake()
        {
            context = GetComponent<AlienShardContext>();
        }

        // Sends scene information to AntAI each time and makes a new plan.
        public void CollectConditions(AntAIAgent aiAgent, AntAICondition worldState)
        {
            bool holdingShard = context.IsHoldingShard();
            bool hasShardTarget = context.targetShard != null;
            bool hasDropTarget = context.dropPoint != null;

            bool nearShard = false;
            if (hasShardTarget)
            {
                nearShard = context.IsNearShardForPickup(context.targetShard);
            }

            bool nearDropPoint = false;
            if (hasDropTarget)
            {
                nearDropPoint = context.IsNearDropPointForDrop();
            }

            worldState.Set(aiAgent.planner, ShardAvailable, context.HasAvailableShard());
            worldState.Set(aiAgent.planner, HasShardTarget, hasShardTarget);
            worldState.Set(aiAgent.planner, NearShard, nearShard);
            worldState.Set(aiAgent.planner, HoldingShard, holdingShard);
            worldState.Set(aiAgent.planner, HasDropTarget, hasDropTarget);
            worldState.Set(aiAgent.planner, NearDropTarget, nearDropPoint);
            worldState.Set(aiAgent.planner, ShardDelivered, context.IsDelivered());
        }
    }
}
