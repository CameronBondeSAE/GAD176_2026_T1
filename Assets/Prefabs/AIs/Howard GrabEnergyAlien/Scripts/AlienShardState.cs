using Anthill.AI;
using UnityEngine;

namespace Howard.ShardAI
{
    public abstract class AlienShardState : AntAIState
    {
        protected AlienShardContext Context;
        protected AlienNavMotor Motor;

        public override void Create(GameObject owner)
        {
            Context = owner.GetComponent<AlienShardContext>();
            Motor = owner.GetComponent<AlienNavMotor>();
        }
    }
}
