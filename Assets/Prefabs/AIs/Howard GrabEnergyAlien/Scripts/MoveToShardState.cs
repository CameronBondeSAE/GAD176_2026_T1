using UnityEngine;

namespace Howard.ShardAI
{
    public class MoveToShardState : AlienShardState
    {
        public override void Enter()
        {
            if (context.targetShard == null ||
                !motor.MoveTo(context.GetShardDestination(context.targetShard), context.pickupDistance))
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

            if (!motor.MoveTo(destination, context.pickupDistance) || motor.PathFailed())
            {
                Fail();
                return;
            }

            if (motor.HasArrived(context.pickupDistance))
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
