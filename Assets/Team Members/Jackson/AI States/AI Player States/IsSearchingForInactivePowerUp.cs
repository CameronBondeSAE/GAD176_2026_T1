using Anthill.AI;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsSearchingForInactivePowerUp : AntAIState
    {
        private GameObject _mainGameObject;
        private AIPlayerSense _aiPlayerSense;
        private GameObject _inactivePowerUp;
    
        public override void Create(GameObject aGameObject)
        {
            _mainGameObject = aGameObject;
            _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
            // Invoke(WaitForABit, 5f) This will allow you to activate a coroutine after waiting a bit
        }

        public override void Enter()
        {
            _inactivePowerUp = _aiPlayerSense.inactivePowerUps[Random.Range(0, _aiPlayerSense.inactivePowerUps.Count)];
            Finish();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("InactivePowerUp found awaiting PowerUp's position");
            Finish();
        }

        public override void Exit()
        {
            _aiPlayerSense.inactivePowerUpTransform = _inactivePowerUp.transform;
            Debug.Log("Found Box Collector at " + _inactivePowerUp.transform.position);
            _aiPlayerSense.foundInactivePowerUp = true;
            Finish();
        }
    }
}
