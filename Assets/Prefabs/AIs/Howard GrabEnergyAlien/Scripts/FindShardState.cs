namespace Howard.ShardAI
{
    public class FindShardState : AlienShardStateBase
    {
        public override void Enter()
        {
            context.AcquireNearestShard();
            Finish();
        }
    }
}
