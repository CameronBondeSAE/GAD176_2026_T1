namespace Howard.ShardAI
{
    public class MoveToDropPointState : AlienShardStateBase
    {
        public override void Enter()
        {
            if (context.dropPoint == null)
            {
                context.AcquireDropPoint();
            }

            if (context.dropPoint == null || !motor.MoveTo(context.dropPoint.position, context.dropDistance))
            {
                Finish();
            }
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            if (!context.IsHoldingShard() || context.dropPoint == null || motor.PathFailed())
            {
                motor.Stop();
                Finish();
                return;
            }

            if (motor.HasArrived(context.dropDistance))
            {
                motor.Stop();
                Finish();
            }
        }
    }
}
