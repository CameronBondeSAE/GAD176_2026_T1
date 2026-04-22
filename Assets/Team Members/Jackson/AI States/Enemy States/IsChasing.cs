using System.Collections;
using Anthill.AI;
using Team_Members.Jackson.AI_Behaviours;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.Enemy_States
{
    public class IsChasing : AntAIState
    {
        private EnemySense _enemySense;
        private TurnTowards _turnTowardsBehaviour;

        public override void Create(GameObject aGameObject)
        {
            _enemySense = aGameObject.GetComponent<EnemySense>();
        }
    
        public override void Enter()
        {
            StartCoroutine(ChaseEnemy());
            Finish();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Chasing Player");
            Finish();
        }

        public override void Exit()
        {
            Debug.Log("Did not kill the player reverting back to wandering");
            Finish();
        }

        private IEnumerator ChaseEnemy()
        {
            _turnTowardsBehaviour.enabled = true;
            yield return new WaitForSeconds(10f);
            _turnTowardsBehaviour.enabled = false;
            yield return new WaitForSeconds(10f);
            _enemySense.seePlayer = false;
        }
    }
}
