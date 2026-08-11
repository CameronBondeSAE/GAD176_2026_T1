using System.Collections.Generic;
using Frank;
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

        public Shard_Model targetShard;
        public Transform dropPoint;
        public NavMeshAgent agent;
        public float deliveredUntil = -1f;

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

        public bool IsDelivered()
        {
            return Time.time < deliveredUntil;
        }

        public bool IsHoldingShard()
        {
            if (interact == null || interact.heldGameObject == null)
            {
                return false;
            }

            return interact.heldGameObject.GetComponentInParent<Shard_Model>() != null;
        }

        // Checks every shard's NavMesh path and reserves the closest reachable one.
        public bool AcquireNearestShard()
        {
            ReleaseShard();

            Shard_Model bestShard = null;
            float bestDistance = float.PositiveInfinity;
            NavMeshPath path = new NavMeshPath();

            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || shard.transform.IsChildOf(transform))
                {
                    continue;
                }

                ShardReservation reservation = shard.GetComponent<ShardReservation>();
                if (reservation == null)
                {
                    reservation = shard.gameObject.AddComponent<ShardReservation>();
                }

                if (!reservation.CanReserve(this))
                {
                    continue;
                }

                bool foundPath = NavMesh.CalculatePath(transform.position, shard.transform.position, agent.areaMask, path);
                if (!foundPath || path.status != NavMeshPathStatus.PathComplete)
                {
                    continue;
                }

                float distance = PathLength(path.corners);
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

            ShardReservation bestReservation = bestShard.GetComponent<ShardReservation>();
            if (!bestReservation.TryReserve(this))
            {
                return false;
            }

            targetShard = bestShard;
            return true;
        }

        // Delivered shards and shards reserved by another alien are not available.
        public bool HasAvailableShard()
        {
            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || shard.transform.IsChildOf(transform))
                {
                    continue;
                }

                ShardReservation reservation = shard.GetComponent<ShardReservation>();
                if (reservation == null || reservation.CanReserve(this))
                {
                    return true;
                }
            }

            return false;
        }

        // Chooses the DropPoint tag with the shortest complete NavMesh path.
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

        public void ReleaseShard()
        {
            if (targetShard != null)
            {
                ShardReservation reservation = targetShard.GetComponent<ShardReservation>();
                if (reservation != null)
                {
                    reservation.Release(this);
                }
            }

            targetShard = null;
        }

        // Marks this shard as delivered so another alien will not collect it again.
        public void CompleteDelivery()
        {
            deliveredUntil = Time.time + deliveredPulseDuration;

            if (targetShard != null)
            {
                ShardReservation reservation = targetShard.GetComponent<ShardReservation>();
                if (reservation == null)
                {
                    reservation = targetShard.gameObject.AddComponent<ShardReservation>();
                }

                reservation.MarkDelivered(this);
            }

            ReleaseShard();
            dropPoint = null;
        }

        // Straight-line distance is misleading around walls, so all NavMesh corners are added together.
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
