using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    public class DropShardState : AlienShardState
    {
        public override void Execute(float deltaTime, float timeScale)
        {
            if (!Context.HoldingShard || Context.DropPoint == null)
            {
                Finish();
                return;
            }
            if (!Motor.Face(Context.DropPoint.position, deltaTime))
                return;

            Vector3 dropPosition = Context.DropPoint.position;
            if (NavMesh.SamplePosition(dropPosition, out NavMeshHit hit, 2f, Context.Agent.areaMask))
                dropPosition = hit.position + Vector3.up * 0.35f;
            if (Context.Interact.TryDrop(dropPosition))
                Context.CompleteDelivery();
            Finish();
        }
    }
}
