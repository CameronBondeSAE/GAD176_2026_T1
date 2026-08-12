using UnityEngine;

namespace Howard.ShardAI
{
    // Records whether this shard still needs delivery and which alien is working on it.
    public class ShardReservation : MonoBehaviour
    {
        public AlienShardContext owner;
        public bool isAtDropPoint;
        public Vector3 dropPosition;
        public float movedDistanceToNeedDelivery = 0.75f;

        public void RefreshState()
        {
            Shard_Model thisShard = GetComponent<Shard_Model>();
            AlienShardContext alienHolder = FindAlienHolder(thisShard);

            if (alienHolder != null)
            {
                isAtDropPoint = false;
                owner = alienHolder;
                return;
            }

            if (isAtDropPoint)
            {
                float movedDistance = Vector3.Distance(transform.position, dropPosition);

                if (movedDistance > movedDistanceToNeedDelivery)
                {
                    isAtDropPoint = false;
                }
            }

            ClearInvalidOwner(thisShard);
        }

        public bool CanReserve(AlienShardContext requester)
        {
            RefreshState();

            if (isAtDropPoint)
            {
                return false;
            }

            return owner == null || owner == requester;
        }

        public bool TryReserve(AlienShardContext requester)
        {
            if (!CanReserve(requester))
            {
                return false;
            }

            owner = requester;
            return true;
        }

        public void MarkCarried(AlienShardContext carrier)
        {
            isAtDropPoint = false;
            owner = carrier;
        }

        public void MarkNeedsDelivery()
        {
            isAtDropPoint = false;
            owner = null;
        }

        public void MarkDelivered(AlienShardContext carrier)
        {
            if (owner != null && owner != carrier)
            {
                return;
            }

            isAtDropPoint = true;
            dropPosition = transform.position;
            owner = null;
        }

        public void Release(AlienShardContext requester)
        {
            if (owner == requester)
            {
                owner = null;
            }
        }

        private void ClearInvalidOwner(Shard_Model thisShard)
        {
            if (owner == null)
            {
                return;
            }

            bool ownerIsActive = owner.isActiveAndEnabled;
            bool ownerStillTargetsShard = owner.targetShard == thisShard;

            if (!ownerIsActive || !ownerStillTargetsShard)
            {
                owner = null;
            }
        }

        private AlienShardContext FindAlienHolder(Shard_Model shard)
        {
            foreach (AlienShardContext alien in FindObjectsByType<AlienShardContext>(FindObjectsSortMode.None))
            {
                if (alien.IsShardInHands(shard))
                {
                    return alien;
                }
            }

            return null;
        }
    }
}
