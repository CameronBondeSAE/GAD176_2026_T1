using Anthill.AI;
using UnityEngine;

namespace Howard.ShardAI
{
    public abstract class AlienShardStateBase : AntAIState
    {
        public AlienShardContext context;
        public AlienNavMotor motor;

        public override void Create(GameObject owner)
        {
            context = owner.GetComponent<AlienShardContext>();
            motor = owner.GetComponent<AlienNavMotor>();
        }
    }
}
