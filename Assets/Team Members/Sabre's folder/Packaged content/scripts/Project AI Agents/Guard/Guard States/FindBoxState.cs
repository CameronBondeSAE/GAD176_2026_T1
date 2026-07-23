using UnityEngine;
using Anthill.AI;
namespace Sabre.AI
{
public class FindBoxState : AntAIState
{
    private GuardSense Senses;
    private Pathfind pathfind;
    private TurnTowards turnTowards;
    private Transform Box;
    private Rigidbody rigidbody;
    [SerializeField] private float MovementSpeed;

    public override void Create(GameObject aGameObject)
    {
        Senses = aGameObject.GetComponent<GuardSense>();
        rigidbody = aGameObject.GetComponent<Rigidbody>();
    }

    public override void Enter()
    {
        Senses.inventory.PickUpOverride = false;
        GameObject pathfindobj = Senses.pathfind;
        pathfind = pathfindobj.GetComponent<Pathfind>();
        turnTowards = pathfindobj.GetComponent<TurnTowards>();

        Box = Senses.boxStation.boxRef.transform;
        pathfind.targetTransform = Box;
        MovementSpeed = pathfind.GetComponent<Stats>().MoveSpeed;

        pathfind.NewTargeting();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Debug.Log("Attempting to go to box");
        pathfind.FollowPath();
        turnTowards.TrackAngle();
        MoveTowards();
    }

    private void MoveTowards()
    {
        rigidbody.AddRelativeForce(0,0,MovementSpeed);
    }
}

}
