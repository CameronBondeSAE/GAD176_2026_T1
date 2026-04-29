using UnityEngine;
using Anthill.AI;
using Team_Members.Jackson.AI_Behaviours;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class WaitForLight : AntAIState
    {
        private MoveForward _moveForwardBehaviour;
        private Wander _wanderBehaviour;
        private Avoid[] _avoidBehaviours;
        private Separation _separationBehaviour;
        private Pathfinder _pathfinderBehaviour;
        
        public override void Create(GameObject aGameObject)
        {
            _moveForwardBehaviour = aGameObject.GetComponent<MoveForward>();
            _wanderBehaviour = aGameObject.GetComponent<Wander>();
            _avoidBehaviours = aGameObject.GetComponentsInChildren<Avoid>();
            _separationBehaviour = aGameObject.GetComponent<Separation>();
            _pathfinderBehaviour = aGameObject.GetComponent<Pathfinder>();
        }

        public override void Enter()
        {
            _moveForwardBehaviour.enabled = false;
            _wanderBehaviour.enabled = false;

            foreach (Avoid avoid in _avoidBehaviours)
            {
                avoid.enabled = false;
            }
            
            _separationBehaviour.enabled = false;
            _pathfinderBehaviour.enabled = false;
        }
    }
}
