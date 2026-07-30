using UnityEngine;
using Anthill.AI;
namespace Sabre.AI
{
public class ReturnBoxState : AntAIState
{
    private GuardSense Senses;
    private Pathfind pathfind;
    private TurnTowards turnTowards;
    private Transform Station;
    private Rigidbody rigidbody;
    [SerializeField] private float MovementSpeed;

    public override void Create(GameObject aGameObject)
    {
        Senses = aGameObject.GetComponent<GuardSense>();
        rigidbody = aGameObject.GetComponent<Rigidbody>();
    }

    public override void Enter()
    {
        GameObject pathfindobj = Senses.pathfind;
        pathfind = pathfindobj.GetComponent<Pathfind>();
        turnTowards = pathfindobj.GetComponent<TurnTowards>();
        
        Station = Senses.boxStation.gameObject.transform;
        
        pathfind.targetTransform = Station;
        MovementSpeed = pathfind.GetComponent<Stats>().MoveSpeed;

        pathfind.NewTargeting();

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        pathfind.FollowPath();
        turnTowards.TrackAngle();

        float DistanceToStation = Vector3.Distance(Station.position, this.gameObject.transform.position);
        Debug.Log("Current distance to station is: " + DistanceToStation);
        if( DistanceToStation <= 4)
        {
            ReturnBox();
            Finish();
            return;
        }
        MoveTowards();
    }

    private void ReturnBox()
    {
        Senses.inventory.PickUpOverride = true;
        if(Senses.inventory.heldCollectable == null)
        {
            Finish();
            return;
        }

        GameObject heldCollectable = Senses.inventory.heldCollectable.gameObject;
        Senses.inventory.DropObject();
        heldCollectable.transform.position = Station.position;
        //Senses.inventory.heldCollectable = null;
        Senses.boxStation.OverrideBoxSafety();
        
    }

    public override void Exit()
    {
        Senses.inventory.PickUpOverride = true;
        
    }

    private void MoveTowards()
    {
        rigidbody.AddRelativeForce(0,0,MovementSpeed);
    }
}
}