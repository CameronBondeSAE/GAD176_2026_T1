using Frank;
using UnityEngine;

namespace Howard.ShardAI
{
    public class PickupShardState : AlienShardState
    {
        public override void Execute(float deltaTime, float timeScale)
        {
            if (context.targetShard == null)
            {
                Finish();
                return;
            }

            Vector3 destination = context.GetShardDestination(context.targetShard);
            float distance = Vector3.Distance(context.transform.position, destination);

            if (distance > context.pickupDistance)
            {
                Finish();
                return;
            }

            if (!motor.Face(context.targetShard.transform.position, deltaTime))
            {
                return;
            }

            if (context.interact.TryPickup(context.targetShard.gameObject))
            {
                ClearPreviousHolder();
                context.NotifyPickedUp(context.targetShard);
                context.AcquireDropPoint();
            }
            else
            {
                context.ReleaseShard();
            }

            Finish();
        }

        // Clears the player's old heldGameObject reference before the alien takes the shard.
        public void ClearPreviousHolder()
        {
            foreach (Interact otherInteract in FindObjectsByType<Interact>(FindObjectsSortMode.None))
            {
                if (otherInteract == context.interact || otherInteract.heldGameObject == null)
                {
                    continue;
                }

                Shard_Model heldShard = otherInteract.heldGameObject.GetComponentInParent<Shard_Model>();

                if (heldShard == context.targetShard)
                {
                    otherInteract.heldGameObject = null;
                }
            }
        }
    }
}
