using Anthill.AI;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsSearchingForDispenser : AntAIState
    {
        private AIPlayerSense _aiPlayerSense;
        private Dispenser _dispenser;
    
        public override void Create(GameObject aGameObject)
        {
            _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
            // Invoke(WaitForABit, 5f) This will allow you to activate a coroutine after waiting a bit
        }

        public override void Enter()
        {
            _dispenser = FindFirstObjectByType<Dispenser>();
            Finish();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Dispenser found awaiting dispenser's position");
            Finish();
        }

        public override void Exit()
        {
            _aiPlayerSense.dispenserTransform = _dispenser.transform;
            Debug.Log("Found Dispenser at " + _dispenser.transform.position);
            _aiPlayerSense.foundDispenser = true;
            _aiPlayerSense.playerWorking = true;
            Finish();
        }
    }
}
