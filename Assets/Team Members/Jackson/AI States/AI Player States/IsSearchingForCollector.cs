using Anthill.AI;
using UnityEngine;

public class IsSearchingForCollector : AntAIState
{
    private GameObject _mainGameObject;
    private AIPlayerSense _aiPlayerSense;
    private BoxCollector _boxCollector;
    
    public override void Create(GameObject aGameObject)
    {
        _mainGameObject = aGameObject;
        _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
        // Invoke(WaitForABit, 5f) This will allow you to activate a coroutine after waiting a bit
    }

    public override void Enter()
    {
        _boxCollector = FindFirstObjectByType<BoxCollector>();
        Finish();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Debug.Log("Box Collector found awaiting box's position");
        Finish();
    }

    public override void Exit()
    {
        _aiPlayerSense.boxCollectorTransform = _boxCollector.transform;
        Debug.Log("Found Box Collector at " + _boxCollector.transform.position);
        _aiPlayerSense.foundCollector = true;
        Finish();
    }
}
