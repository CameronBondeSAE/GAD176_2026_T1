using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    public class DropShardState : AlienShardStateBase
    {
        public override void Execute(float deltaTime, float timeScale)
        {
            Shard_Model heldShard = context.GetHeldShard();

            if (heldShard == null || context.dropPoint == null)
            {
                Finish();
                return;
            }

            if (!motor.Face(context.dropPoint.position, deltaTime))
            {
                return;
            }

            Vector3 dropPosition = context.dropPoint.position;
            if (NavMesh.SamplePosition(dropPosition, out NavMeshHit hit, 2f, context.agent.areaMask))
            {
                dropPosition = hit.position + Vector3.up * 0.35f;
            }

            if (context.interact.TryDrop(dropPosition))
            {
                context.CompleteDelivery(heldShard);
            }
            
            Finish();
        }
    }
}
