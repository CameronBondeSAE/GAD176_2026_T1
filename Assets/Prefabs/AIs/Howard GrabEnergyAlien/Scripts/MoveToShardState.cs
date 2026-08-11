namespace Howard.ShardAI
{
    public class MoveToShardState : AlienShardState
    {
        public override void Enter()
        {
            if (context.targetShard == null || !motor.MoveTo(context.targetShard.transform.position, context.pickupDistance))
            {
                Fail();
            }
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            if (context.targetShard == null || motor.PathFailed())
            {
                Fail();
                return;
            }

            motor.MoveTo(context.targetShard.transform.position, context.pickupDistance);

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
