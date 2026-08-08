namespace Howard.ShardAI
{
    public class MoveToShardState : AlienShardState
    {
        public override void Enter()
        {
            if (Context.TargetShard == null || !Motor.MoveTo(Context.TargetShard.transform.position, Context.PickupDistance))
                Fail();
        }

        public override void Execute(float deltaTime, float timeScale)
        {
            if (Context.TargetShard == null || Motor.PathFailed)
            {
                Fail();
                return;
            }
            Motor.MoveTo(Context.TargetShard.transform.position, Context.PickupDistance);
            if (Motor.HasArrived(Context.PickupDistance))
            {
                Motor.Stop();
                Finish();
            }
        }

        private void Fail()
        {
            Motor.Stop();
            Context.ReleaseShard();
            Finish();
        }
    }
}
