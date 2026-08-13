using UnityEngine;

namespace Nicholas.AI
{
    public class Model_AlienHunterAI : MonoBehaviour
    {
        public Transform CurrentPlayerTarget { get; private set; }

        public bool HasPatrolTarget { get; private set; }
        public bool AtPatrolTarget { get; private set; }
        public bool SearchComplete { get; private set; }

        public bool CanSeePlayer { get; private set; }
        public bool HasPlayerTarget { get; private set; }
        public bool IsTouchingPlayer { get; private set; }
        public bool PlayerKilled { get; private set; } = false;
        

        public void SetPatrolTargetState(bool hasTarget)
        {
            HasPatrolTarget = hasTarget;
        }

        public void SetAtPatrolTarget(bool atTarget)
        {
            AtPatrolTarget = atTarget;
        }

        public void SetSearchComplete(bool complete)
        {
            SearchComplete = complete;
        }

        public void SetPlayerTarget(Transform playerTarget)
        {
            CurrentPlayerTarget = playerTarget;

            CanSeePlayer = playerTarget != null;
            HasPlayerTarget = playerTarget != null;
        }

        public void SetCanSeePlayer(bool canSeePlayer)
        {
            CanSeePlayer = canSeePlayer;
        }

        public void ClearPlayerTarget()
        {
            CurrentPlayerTarget = null;

            CanSeePlayer = false;
            HasPlayerTarget = false;
        }

        public void SetTouchingPlayer(bool touching)
        {
            IsTouchingPlayer = touching;
        }

        public void SetPlayerKilled(bool killed)
        {
            PlayerKilled = killed;
        }
    }
}