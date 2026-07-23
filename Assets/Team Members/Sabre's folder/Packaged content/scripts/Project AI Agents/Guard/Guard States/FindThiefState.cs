using UnityEngine;
using Anthill.AI;
namespace Sabre.AI
{
public class FindThiefState : AntAIState
{
    
    private GuardSense Senses;
    private Pathfind pathfind;
    private TurnTowards turnTowards;
    private Transform Target;
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

        pathfind.targetTransform = Senses.CurrentTarget.transform;
        MovementSpeed = pathfind.GetComponent<Stats>().MoveSpeed;

        pathfind.NewTargeting();
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        Debug.Log("Attempting to go to box");
        pathfind.FollowPath();
        turnTowards.TrackAngle();
        MoveTowards();

        float dist = Vector3.Distance(Senses.CurrentTarget.transform.position, transform.position);
        if(dist < 5f)
        {
            Senses.FightThief = true;
            Exit();
        }
    }

    private void MoveTowards()
    {
        rigidbody.AddRelativeForce(0,0,MovementSpeed);
    }

}
}