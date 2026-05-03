using Anthill.AI;
using Team_Members.Jackson.AI_Behaviours;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsWandering : AntAIState
    {
        private PowerUp _powerUp;
        private MoveForward _moveForwardBehaviour;
        private Wander _wanderBehaviour;
        private Avoid[] _avoidBehaviours;
        private Separation _separationBehaviour;

        private AIPlayerSense _aiPlayerSense;

        public override void Create(GameObject aGameObject)
        {
            _moveForwardBehaviour = aGameObject.GetComponent<MoveForward>();
            _wanderBehaviour = aGameObject.GetComponent<Wander>();
            _avoidBehaviours = aGameObject.GetComponentsInChildren<Avoid>();
            _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
        }

        public override void Enter()
        {
            _aiPlayerSense.inactivePowerUps.Clear();
            _moveForwardBehaviour.enabled = true;
            _wanderBehaviour.enabled = true;

            foreach (Avoid avoid in _avoidBehaviours)
            {
                avoid.enabled = true;
            }
            
            foreach (PowerUp powerUp in FindObjectsByType<PowerUp>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (!powerUp.gameObject.activeInHierarchy)
                {
                    _aiPlayerSense.inactivePowerUps.Add(powerUp.gameObject);
                }
                else
                {
                    continue;
                }
            }
            
            Finish();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Wandering until an Energy PowerUp has been picked up");
            Finish();
        }

        public override void Exit()
        {
            Debug.Log(_aiPlayerSense.inactivePowerUps.Count);
            if (_aiPlayerSense.inactivePowerUps.Count > 0)
            {
                _aiPlayerSense.missingPowerUp = true;
            }
        
            Debug.Log("Found Inactive PowerUp moving on to searching for the dispenser");
            Finish();
        }
    }
}
