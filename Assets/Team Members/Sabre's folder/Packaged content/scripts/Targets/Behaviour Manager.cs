using UnityEngine;
namespace Sabre.AI

{
    public class BehaviourManager : MonoBehaviour
    {
        public Pathfind pathFindRef;
        public TurnTowards turnTowardsRef;
        public MoveForward moveForwardRef;
        public Avoid frontAvoidRef;
        public Avoid leftAvoidRef;
        public Avoid rightAvoidRef;
        public Align allignRef;
        public Seperation seperationRef;
    }
}