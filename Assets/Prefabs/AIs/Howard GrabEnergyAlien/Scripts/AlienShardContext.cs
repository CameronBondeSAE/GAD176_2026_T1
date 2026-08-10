using System.Collections.Generic;
using Frank;
using UnityEngine;
using UnityEngine.AI;

namespace Howard.ShardAI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AlienShardContext : MonoBehaviour
    {
        [SerializeField] private Interact interact;
        [SerializeField] private Transform hands;
        [SerializeField] private float pickupDistance = 1.35f;
        [SerializeField] private float dropDistance = 1.5f;
        [SerializeField] private float deliveredPulseDuration = 0.3f;

        public Shard_Model TargetShard { get; private set; }
        public Transform DropPoint { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Interact Interact => interact;
        public Transform Hands => hands;
        public float PickupDistance => pickupDistance;
        public float DropDistance => dropDistance;
        public bool Delivered => Time.time < _deliveredUntil;
        public bool HoldingShard => interact != null && interact.heldGameObject != null &&
                                    interact.heldGameObject.GetComponentInParent<Shard_Model>() != null;

        private float _deliveredUntil = -1f;

        private void Awake()
        {
            Agent = GetComponent<NavMeshAgent>();
            if (interact == null)
                interact = GetComponentInChildren<Interact>(true);
            if (hands == null && interact != null)
                hands = interact.handsTransform;
        }

        public bool AcquireNearestShard()
        {
            ReleaseShard();
            Shard_Model best = null;
            float bestDistance = float.PositiveInfinity;
            var path = new NavMeshPath();

            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || shard.transform.IsChildOf(transform))
                    continue;

                ShardReservation reservation = shard.GetComponent<ShardReservation>();
                if (reservation == null)
                    reservation = shard.gameObject.AddComponent<ShardReservation>();
                if (!reservation.CanReserve(this))
                    continue;
                if (!NavMesh.CalculatePath(transform.position, shard.transform.position, Agent.areaMask, path) ||
                    path.status != NavMeshPathStatus.PathComplete)
                    continue;

                float distance = PathLength(path.corners);
                if (distance < bestDistance)
                {
                    best = shard;
                    bestDistance = distance;
                }
            }

            if (best == null)
                return false;

            ShardReservation bestReservation = best.GetComponent<ShardReservation>();
            if (!bestReservation.TryReserve(this))
                return false;
            TargetShard = best;
            return true;
        }

        public bool HasAvailableShard()
        {
            foreach (Shard_Model shard in FindObjectsByType<Shard_Model>(FindObjectsSortMode.None))
            {
                if (shard == null || shard.transform.IsChildOf(transform))
                    continue;

                ShardReservation reservation = shard.GetComponent<ShardReservation>();
                if (reservation == null || reservation.CanReserve(this))
                    return true;
            }

            return false;
        }

        public bool AcquireDropPoint()
        {
            DropPoint = null;
            float bestDistance = float.PositiveInfinity;
            var path = new NavMeshPath();

            foreach (GameObject point in GameObject.FindGameObjectsWithTag("DropPoint"))
            {
                if (!NavMesh.CalculatePath(transform.position, point.transform.position, Agent.areaMask, path) ||
                    path.status != NavMeshPathStatus.PathComplete)
                    continue;
                float distance = PathLength(path.corners);
                if (distance < bestDistance)
                {
                    DropPoint = point.transform;
                    bestDistance = distance;
                }
            }
            return DropPoint != null;
        }

        public void ReleaseShard()
        {
            if (TargetShard != null)
            {
                ShardReservation reservation = TargetShard.GetComponent<ShardReservation>();
                if (reservation != null)
                    reservation.Release(this);
            }
            TargetShard = null;
        }

        public void CompleteDelivery()
        {
            _deliveredUntil = Time.time + deliveredPulseDuration;

            if (TargetShard != null)
            {
                ShardReservation reservation = TargetShard.GetComponent<ShardReservation>();
                if (reservation == null)
                    reservation = TargetShard.gameObject.AddComponent<ShardReservation>();
                reservation.MarkDelivered(this);
            }

            ReleaseShard();
            DropPoint = null;
        }

        private static float PathLength(IReadOnlyList<Vector3> corners)
        {
            float result = 0f;
            for (int i = 1; i < corners.Count; i++)
                result += Vector3.Distance(corners[i - 1], corners[i]);
            return result;
        }
    }
}
