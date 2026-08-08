using UnityEngine;

namespace Howard.ShardAI
{
    public class ShardReservation : MonoBehaviour
    {
        private AlienShardContext _owner;

        public bool IsDelivered { get; private set; }

        public bool CanReserve(AlienShardContext requester) =>
            !IsDelivered && (_owner == null || _owner == requester);

        public bool TryReserve(AlienShardContext requester)
        {
            if (!CanReserve(requester))
                return false;
            _owner = requester;
            return true;
        }

        public void Release(AlienShardContext requester)
        {
            if (_owner == requester)
                _owner = null;
        }

        public void MarkDelivered(AlienShardContext requester)
        {
            if (_owner != null && _owner != requester)
                return;

            IsDelivered = true;
            _owner = null;
        }
    }
}
