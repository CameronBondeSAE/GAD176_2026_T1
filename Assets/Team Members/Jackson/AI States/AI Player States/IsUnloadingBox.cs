using Anthill.AI;
using UnityEngine;

public class IsUnloadingBox : AntAIState
{
    private AIPlayerSense _aiPlayerSense;
    private MoveForward _moveForward;
    private Wander _wanderBehaviour;
    private Avoid[] _avoidBehaviours;
    private Pathfinder _pathfinder;
    private BoxCollector _boxCollector;
    private GameObject _mainGameObject;
    
    public override void Create(GameObject aGameObject)
    {
        _aiPlayerSense = aGameObject.GetComponent<AIPlayerSense>();
        _moveForward = aGameObject.GetComponent<MoveForward>();
        _wanderBehaviour = aGameObject.GetComponent<Wander>();
        _avoidBehaviours = aGameObject.GetComponentsInChildren<Avoid>();
        _pathfinder = aGameObject.GetComponent<Pathfinder>();
        _mainGameObject = aGameObject;
    }

    public override void Enter()
    {
        _boxCollector = FindFirstObjectByType<BoxCollector>();
        _pathfinder.targetTransform = _boxCollector.transform;
        _aiPlayerSense.audioSource.clip = _aiPlayerSense.boxDeliveredClip;
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
        _aiPlayerSense.backpack.SetActive(false);
        _aiPlayerSense.boxSpawned = false;
        _aiPlayerSense.foundBox = false;
        _aiPlayerSense.playerWorking = false;
        _aiPlayerSense.playerHasBox = false;
        _aiPlayerSense.foundCollector = false;
        
        foreach (Avoid avoid in _avoidBehaviours)
        {
            avoid.enabled = true;
        }
        
        _pathfinder.enabled = false;
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Debug.Log("Received collector's location starting to pathfind towards the collector");
        
        _pathfinder.ExecutePath();
        
        Finish();
    }

    public override void Exit()
    {
        _pathfinder.reachedEndEvent.RemoveListener(ReachedEnd);
        
        Finish();
    }
}
