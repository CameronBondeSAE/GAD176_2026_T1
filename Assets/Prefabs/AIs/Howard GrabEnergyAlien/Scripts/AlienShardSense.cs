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

        private AlienShardContext _context;

        private void Awake() => _context = GetComponent<AlienShardContext>();

        public void CollectConditions(AntAIAgent aAgent, AntAICondition worldState)
        {
            bool holding = _context.HoldingShard;
            bool hasShard = _context.TargetShard != null;
            bool hasDrop = _context.DropPoint != null;
            worldState.Set(aAgent.planner, ShardAvailable, _context.HasAvailableShard());
            worldState.Set(aAgent.planner, HasShardTarget, hasShard);
            worldState.Set(aAgent.planner, NearShard, hasShard &&
                Vector3.Distance(transform.position, _context.TargetShard.transform.position) <= _context.PickupDistance);
            worldState.Set(aAgent.planner, HoldingShard, holding);
            worldState.Set(aAgent.planner, HasDropTarget, hasDrop);
            worldState.Set(aAgent.planner, NearDropTarget, hasDrop &&
                Vector3.Distance(transform.position, _context.DropPoint.transform.position) <= _context.DropDistance);
            worldState.Set(aAgent.planner, ShardDelivered, _context.Delivered);
        }
    }
}
