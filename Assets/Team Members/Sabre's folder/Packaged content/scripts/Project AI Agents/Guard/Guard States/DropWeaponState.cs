using UnityEngine;
using Anthill.AI;
namespace Sabre.AI
{
public class DropWeaponState : AntAIState
{
     private GuardSense Senses;
    private TurnTowards turnTowards;
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
        turnTowards = pathfindobj.GetComponent<TurnTowards>();
        
        MovementSpeed = pathfindobj.GetComponent<Stats>().MoveSpeed;

    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        turnTowards.TrackAngle();
        ReturnBox();
        
    }

    private void ReturnBox()
    {
        GameObject heldCollectable = Senses.inventory.heldCollectable.gameObject;
        Senses.inventory.DropObject();
        //Finish();
    }
}
}