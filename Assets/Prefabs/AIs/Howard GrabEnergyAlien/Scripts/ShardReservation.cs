using UnityEngine;

namespace Howard.ShardAI
{
    // Stops two aliens from choosing the same shard and stops delivered shards being collected again.
    public class ShardReservation : MonoBehaviour
    {
        public AlienShardContext owner;
        public bool isDelivered;

        public bool CanReserve(AlienShardContext requester)
        {
            bool hasNoOwner = owner == null;
            bool ownedByRequester = owner == requester;
            return !isDelivered && (hasNoOwner || ownedByRequester);
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

        public void Release(AlienShardContext requester)
        {
            if (owner == requester)
            {
                owner = null;
            }
        }

        public void MarkDelivered(AlienShardContext requester)
        {
            if (owner != null && owner != requester)
            {
                return;
            }

            isDelivered = true;
            owner = null;
        }
    }
}
