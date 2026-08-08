namespace Howard.ShardAI
{
    public class PickupShardState : AlienShardState
    {
        public override void Execute(float deltaTime, float timeScale)
        {
            if (Context.TargetShard == null)
            {
                Finish();
                return;
            }
            if (!Motor.Face(Context.TargetShard.transform.position, deltaTime))
                return;
            if (Context.Interact.TryPickup(Context.TargetShard.gameObject))
                Context.AcquireDropPoint();
            else
                Context.ReleaseShard();
            Finish();
        }
    }
}
