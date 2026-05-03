using Anthill.AI;
using Team_Members.Jackson.AI_Behaviours;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsCollectingPowerUp : AntAIState
    {
        private AIPlayerSense _aiPlayerSense;
        private MoveForward _moveForward;
        private Wander _wanderBehaviour;
        private Avoid[] _avoidBehaviours;
        private Pathfinder _pathfinder;
    
        public override void Create(GameObject aGameObject)
        {
            _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
            _moveForward = aGameObject.GetComponent<MoveForward>();
            _wanderBehaviour = aGameObject.GetComponent<Wander>();
            _avoidBehaviours = aGameObject.GetComponentsInChildren<Avoid>();
            _pathfinder = aGameObject.GetComponent<Pathfinder>();
        }

        public override void Enter()
        {
            _pathfinder.targetTransform = _aiPlayerSense.dispenserTransform;
            _aiPlayerSense.audioSource.clip = _aiPlayerSense.boxCollectedClip;
            _wanderBehaviour.enabled = false;

            foreach (Avoid avoid in _avoidBehaviours)
            {
                avoid.enabled = false;
            }

            _pathfinder.enabled = true;
            _pathfinder.CalculatePath();
            _pathfinder.reachedEndEvent.AddListener(ReachedEnd);
        }

        private void ReachedEnd()
        {
            _aiPlayerSense.audioSource.Play();
            _aiPlayerSense.backpack.SetActive(true);
            _aiPlayerSense.playerHasPowerUp = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Received dispenser's location starting to pathfind towards the dispenser");
        
            _pathfinder.ExecutePath();
        
            Finish();
        }

        public override void Exit()
        {
            foreach (Avoid avoid in _avoidBehaviours)
            {
                avoid.enabled = true;
            }
        
            _pathfinder.enabled = false;
            _moveForward.enabled = true;
            _wanderBehaviour.enabled = true;

            _pathfinder.reachedEndEvent.RemoveListener(ReachedEnd);
        
            Finish();
        }
    }
}
