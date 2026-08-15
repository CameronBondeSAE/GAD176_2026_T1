using UnityEngine;

namespace Howard.ShardAI
{
    public class MoveToShardState : AlienShardStateBase
    {
        public override void Enter()
        {
            if (context.targetShard == null || !motor.MoveTo(context.GetShardDestination(context.targetShard), context.moveStoppingDistance))
            {
                Fail();
            }
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            context.ReviewShardTarget();

            if (context.targetShard == null)
            {
                Fail();
                return;
            }

            Vector3 destination = context.GetShardDestination(context.targetShard);

            if (!motor.MoveTo(destination, context.moveStoppingDistance) || motor.PathFailed())
            {
                Fail();
                return;
            }

            if (context.IsNearShardForPickup(context.targetShard) || motor.HasArrived(context.moveStoppingDistance))
            {
                motor.Stop();
                Finish();
            }
        }

        private void Fail()
        {
            motor.Stop();
            context.ReleaseShard();
            Finish();
        }
    }
}
