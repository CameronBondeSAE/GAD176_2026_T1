namespace Howard.ShardAI
{
    public class FindShardState : AlienShardState
    {
        public override void Enter()
        {
            Context.AcquireNearestShard();
            Finish();
        }
    }
}
