namespace Howard.ShardAI
{
    public class MoveToDropPointState : AlienShardState
    {
        public override void Enter()
        {
            if (Context.DropPoint == null)
                Context.AcquireDropPoint();
            if (Context.DropPoint == null || !Motor.MoveTo(Context.DropPoint.position, Context.DropDistance))
                Finish();
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            if (!Context.HoldingShard || Context.DropPoint == null || Motor.PathFailed)
            {
                Motor.Stop();
                Finish();
                return;
            }
            if (Motor.HasArrived(Context.DropDistance))
            {
                Motor.Stop();
                Finish();
            }
        }
    }
}
