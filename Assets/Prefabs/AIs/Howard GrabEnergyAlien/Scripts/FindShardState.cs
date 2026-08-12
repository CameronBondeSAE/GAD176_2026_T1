namespace Howard.ShardAI
{
    public class FindShardState : AlienShardState
    {
        public override void Enter()
        {
            context.AcquireNearestShard();
            Finish();
        }
    }
}
