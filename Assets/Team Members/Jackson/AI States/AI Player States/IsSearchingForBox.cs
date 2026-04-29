using Anthill.AI;
using Team_Members.Jackson.AI_Senses;
using UnityEngine;

namespace Team_Members.Jackson.AI_States.AI_Player_States
{
    public class IsSearchingForBox : AntAIState
    {
        private GameObject _mainGameObject;
        private AIPlayerSense _aiPlayerSense;
        private Box _box;
    
        public override void Create(GameObject aGameObject)
        {
            _mainGameObject = aGameObject;
            _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
            // Invoke(WaitForABit, 5f) This will allow you to activate a coroutine after waiting a bit
        }

        public override void Enter()
        {
            _box = FindFirstObjectByType<Box>(FindObjectsInactive.Include);
            _box.gameObject.SetActive(true);
            
            Finish();
        }

        public override void Execute(float aDeltaTime, float aTimeScale)
        {
            Debug.Log("Box found awaiting box's position");
            Finish();
        }

        public override void Exit()
        {
            _aiPlayerSense.boxTransform = _box.transform;
            Debug.Log("Found Box at " + _box.transform.position);
            _aiPlayerSense.foundBox = true;
            _aiPlayerSense.playerWorking = true;
            Finish();
        }
    }
}
