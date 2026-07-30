using UnityEngine;
using Anthill.AI;
namespace Sabre.AI
{
public class PatrolState : AntAIState
{
    private GuardSense Senses;
    private Pathfind pathfind;
    private TurnTowards turnTowards;
    private Rigidbody rigidbody;
    [SerializeField] private float MovementSpeed;
    

    public override void Create(GameObject aGameObject)
    {
        Senses = aGameObject.GetComponent<GuardSense>();

        rigidbody = aGameObject.GetComponent<Rigidbody>();

        GameObject pathfindobj = Senses.pathfind;
        pathfind = pathfindobj.GetComponent<Pathfind>();
        turnTowards = pathfindobj.GetComponent<TurnTowards>();
        
    }

    public override void Enter()
    {
        pathfind.preTarget = true;
        pathfind.targetManager = Senses.boxStation.gameObject.GetComponent<TargetManager>();
        pathfind.PickTarget();
        Senses.inventory.PickUpOverride = true;
        
        MovementSpeed = pathfind.GetComponent<Stats>().MoveSpeed;
        
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Debug.Log("executing patrol");
        MoveTowards();
        pathfind.FollowPath();
        turnTowards.TrackAngle();
        
    }

    private void MoveTowards()
    {
        rigidbody.AddRelativeForce(0,0,MovementSpeed);
    }

    public override void Exit()
    {
        pathfind.targetManager = null;
        pathfind.preTarget = false;
        Senses.inventory.PickUpOverride = false;
    }
}
    




}

