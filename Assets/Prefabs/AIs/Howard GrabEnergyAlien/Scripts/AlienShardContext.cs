using System.Collections.Generic;
using Frank;
using Keegan.FOV;
using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AlienShardContext : MonoBehaviour
    {
        public Interact interact;
        public Transform hands;
        public float pickupDistance = 1.35f;
        public float dropDistance = 1.5f;
        public float deliveredPulseDuration = 0.3f;
        public float targetCheckInterval = 0.25f;
        public float targetSwitchDistance = 1.5f;
        public float targetNavMeshSearchDistance = 3f;

        public Shard_Model targetShard;
        public Shard_Model lastHeldShard;
        public Transform dropPoint;
        public NavMeshAgent agent;
        public float deliveredUntil = -1f;
        public float nextTargetCheckTime;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

            if (interact == null)
            {
                interact = GetComponentInChildren<Interact>(true);
            }

            if (hands == null && interact != null)
            {
                hands = interact.handsTransform;
            }
        }

        private void Update()
        {
            UpdateCarryingState();
        }

        private void OnDisable()
        {
            ReleaseShard();
        }

        public bool IsDelivered()
        {
            return Time.time < deliveredUntil;
        }

        // heldGameObject can be stale after another Interact reparents the shard.
        public Shard_Model GetHeldShard()
        {
            if (interact == null || interact.heldGameObject == null || hands == null)
            {
                return null;
            }

            Shard_Model heldShard = interact.heldGameObject.GetComponentInParent<Shard_Model>();

            if (heldShard != null && heldShard.transform.IsChildOf(hands))
            {
                return heldShard;
            }

            interact.heldGameObject = null;
            return null;
        }

        public bool IsHoldingShard()
        {
            return GetHeldShard() != null;
        }

        public bool IsShardInHands(Shard_Model shard)
        {
            if (shard == null || hands == null)
            {
                return false;
            }

            return shard.transform.IsChildOf(hands);
        }

        public void UpdateCarryingState()
        {
            Shard_Model heldShard = GetHeldShard();

            if (lastHeldShard != null && heldShard != lastHeldShard)
            {
                GetReservation(lastHeldShard).MarkNeedsDelivery();

                if (targetShard == lastHeldShard)
                {
                    targetShard = null;
                }
            }

            if (heldShard != null)
            {
                GetReservation(heldShard).MarkCarried(this);
                targetShard = heldShard;
            }

            lastHeldShard = heldShard;
        }

        public bool AcquireNearestShard()
        {
            UpdateCarryingState();

            if (IsHoldingShard())
            {
                return false;
            }

            ReleaseShard();

            Shard_Model bestShard = null;
            float bestDistance = float.PositiveInfinity;

            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || IsShardHeldByAnyAlien(shard))
                {
                    continue;
                }

                ShardReservation reservation = GetReservation(shard);

                if (!reservation.CanReserve(this))
                {
                    continue;
                }

                if (!TryGetPathDistance(shard, out float distance))
                {
                    continue;
                }

                if (distance < bestDistance)
                {
                    bestShard = shard;
                    bestDistance = distance;
                }
            }

            if (bestShard == null)
            {
                return false;
            }

            if (!GetReservation(bestShard).TryReserve(this))
            {
                return false;
            }

            targetShard = bestShard;
            nextTargetCheckTime = Time.time + targetCheckInterval;
            return true;
        }

        // Changes target only when the new path is clearly shorter than the current path.
        public void ReviewShardTarget()
        {
            if (Time.time < nextTargetCheckTime || IsHoldingShard())
            {
                return;
            }

            nextTargetCheckTime = Time.time + targetCheckInterval;

            Shard_Model currentShard = targetShard;
            Shard_Model bestShard = null;
            float currentDistance = float.PositiveInfinity;
            float bestDistance = float.PositiveInfinity;
            bool currentShardIsUsable = false;

            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || IsShardHeldByAnyAlien(shard))
                {
                    continue;
                }

                ShardReservation reservation = GetReservation(shard);

                if (!reservation.CanReserve(this))
                {
                    continue;
                }

                if (!TryGetPathDistance(shard, out float distance))
                {
                    continue;
                }

                if (shard == currentShard)
                {
                    currentShardIsUsable = true;
                    currentDistance = distance;
                }

                if (distance < bestDistance)
                {
                    bestShard = shard;
                    bestDistance = distance;
                }
            }

            if (bestShard == null)
            {
                ReleaseShard();
                return;
            }

            if (bestShard == currentShard)
            {
                return;
            }

            if (currentShardIsUsable && bestDistance + targetSwitchDistance >= currentDistance)
            {
                return;
            }

            ReleaseShard();

            if (GetReservation(bestShard).TryReserve(this))
            {
                targetShard = bestShard;
            }
        }

        public bool HasAvailableShard()
        {
            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || IsShardHeldByAnyAlien(shard))
                {
                    continue;
                }

                ShardReservation reservation = GetReservation(shard);

                if (reservation.CanReserve(this) && TryGetPathDistance(shard, out float distance))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsShardHeldByAnyAlien(Shard_Model shard)
        {
            foreach (AlienShardContext alien in FindObjectsByType<AlienShardContext>(FindObjectsSortMode.None))
            {
                if (alien.IsShardInHands(shard))
                {
                    return true;
                }
            }

            return false;
        }

        public bool AcquireDropPoint()
        {
            dropPoint = null;
            float bestDistance = float.PositiveInfinity;
            NavMeshPath path = new NavMeshPath();

            foreach (GameObject point in GameObject.FindGameObjectsWithTag("DropPoint"))
            {
                bool foundPath = NavMesh.CalculatePath(transform.position, point.transform.position, agent.areaMask, path);

                if (!foundPath || path.status != NavMeshPathStatus.PathComplete)
                {
                    continue;
                }

                float distance = PathLength(path.corners);

                if (distance < bestDistance)
                {
                    dropPoint = point.transform;
                    bestDistance = distance;
                }
            }

            return dropPoint != null;
        }

        public Vector3 GetShardDestination(Shard_Model shard)
        {
            if (shard != null && NavMesh.SamplePosition(shard.transform.position, out NavMeshHit hit,
                    targetNavMeshSearchDistance, agent.areaMask))
            {
                return hit.position;
            }

            return shard == null ? transform.position : shard.transform.position;
        }

        public bool TryGetPathDistance(Shard_Model shard, out float distance)
        {
            distance = float.PositiveInfinity;

            if (shard == null || agent == null || !agent.isOnNavMesh)
            {
                return false;
            }

            Vector3 destination = GetShardDestination(shard);
            NavMeshPath path = new NavMeshPath();
            bool foundPath = NavMesh.CalculatePath(transform.position, destination, agent.areaMask, path);

            if (!foundPath || path.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }

            distance = PathLength(path.corners);
            return true;
        }

        public void NotifyPickedUp(Shard_Model shard)
        {
            targetShard = shard;
            lastHeldShard = shard;
            GetReservation(shard).MarkCarried(this);
        }

        public void ReleaseShard()
        {
            if (targetShard != null)
            {
                GetReservation(targetShard).Release(this);
            }

            targetShard = null;
        }

        public void CompleteDelivery(Shard_Model deliveredShard)
        {
            deliveredUntil = Time.time + deliveredPulseDuration;

            if (deliveredShard != null)
            {
                GetReservation(deliveredShard).MarkDelivered(this);
            }

            lastHeldShard = null;
            targetShard = null;
            dropPoint = null;
        }

        public ShardReservation GetReservation(Shard_Model shard)
        {
            ShardReservation reservation = shard.GetComponent<ShardReservation>();

            if (reservation == null)
            {
                reservation = shard.gameObject.AddComponent<ShardReservation>();
            }

            return reservation;
        }

        private float PathLength(IReadOnlyList<Vector3> corners)
        {
            float totalDistance = 0f;

            for (int i = 1; i < corners.Count; i++)
            {
                totalDistance += Vector3.Distance(corners[i - 1], corners[i]);
            }

            return totalDistance;
        }
    }
}
