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

            if (!motor.Face(context.targetShard.transform.position, deltaTime))
            {
                return;
            }

            if (context.interact.TryPickup(context.targetShard.gameObject))
            {
                context.AcquireDropPoint();
            }
            else
            {
                context.ReleaseShard();
            }

            Finish();
        }
    }
}
