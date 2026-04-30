using Anthill.AI;
using Team_Members.Jackson.AI_Behaviours;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsWandering : AntAIState
    {
        private Box _box;
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
            _separationBehaviour = aGameObject.GetComponent<Separation>();
            _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
            _box = FindFirstObjectByType<Box>(FindObjectsInactive.Include);
        }

        public override void Enter()
        {
            _moveForwardBehaviour.enabled = true;
            _wanderBehaviour.enabled = true;

            foreach (Avoid avoid in _avoidBehaviours)
            {
                avoid.enabled = true;
            }
        
            _separationBehaviour.enabled = true;
            Finish();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Wandering until a box has spawned");
            Finish();
        }

        public override void Exit()
        {
            _box.gameObject.SetActive(true);
            _aiPlayerSense.boxSpawned = true;
        
            Debug.Log("Box Has Spawned now moving on to Searching for the Box");
            Finish();
        }
    }
}
