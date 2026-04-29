using Anthill.AI;
using Team_Members.Jackson.AI_Behaviours;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsCollectingBox : AntAIState
    {
        private AIPlayerSense _aiPlayerSense;
        private MoveForward _moveForward;
        private Wander _wanderBehaviour;
        private Avoid[] _avoidBehaviours;
        private Pathfinder _pathfinder;
        private Box _box;
    
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
            _box = FindFirstObjectByType<Box>();
            _pathfinder.targetTransform = _box.transform;
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
            _box.gameObject.SetActive(false);
            _aiPlayerSense.backpack.SetActive(true);
            _aiPlayerSense.playerHasBox = true;
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Received box's location starting to pathfind towards the box");
        
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
