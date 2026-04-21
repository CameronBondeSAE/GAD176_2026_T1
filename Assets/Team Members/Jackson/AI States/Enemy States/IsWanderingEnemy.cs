using Anthill.AI;
using UnityEngine;

public class IsWanderingEnemy : AntAIState
{
    private EnemySense _enemySense;
    private MoveForward _moveForwardBehaviour;
    private Wander _wanderBehaviour;
    private Avoid[] _avoidBehaviours;
    private Vision _vision;
    
    public override void Create(GameObject aGameObject)
    {
        _enemySense = aGameObject.GetComponent<EnemySense>();
        _moveForwardBehaviour = aGameObject.GetComponent<MoveForward>();
        _wanderBehaviour = aGameObject.GetComponent<Wander>();
        _avoidBehaviours = aGameObject.GetComponentsInChildren<Avoid>();
        _vision = aGameObject.GetComponent<Vision>();
    }
    
    public override void Enter()
    {
        _moveForwardBehaviour.enabled = true;
        _wanderBehaviour.enabled = true;

        foreach (Avoid avoid in _avoidBehaviours)
        {
            avoid.enabled = true;
        }

        _vision.enabled = true;

        Finish();
    }
    
    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Debug.Log("Wandering until in line of sight of the player");
        Finish();
    }
    
    public override void Exit()
    {
        if (_vision.canSeePlayer)
        {
            _enemySense.seePlayer = true;
            Debug.Log("Found Player moving on to Chasing the Player");
        }

        Finish();
    }
}
