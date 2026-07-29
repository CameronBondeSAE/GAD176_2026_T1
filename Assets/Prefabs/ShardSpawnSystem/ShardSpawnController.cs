using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Keegan.ShardSpawn
{
    public class ShardSpawnController : MonoBehaviour
    {
        public enum RespawnType
        {
            None,
            Loop,
            Collect
        }
        
        
        [SerializeField, Tooltip("The prefab for the shard that will spawn")]
        private GameObject shardPrefab;

        [Header("Spawn Time Settings")]
        [SerializeField, Tooltip("The min amount of time before a shard will spawn (again)")]
        private float minSpawnTime = 20f;

        [SerializeField, Tooltip("The max amount of time before a shard will spawn (again")]
        private float maxSpawnTime = 100f;
        
        [SerializeField, Tooltip("Reference to the transform that the shard will spawn on")]
        private Transform spawnOnTransform;

        [SerializeField, Tooltip("Bounds for the range to spawn a shard in")]
        private Vector3 spawnBoxBounds = Vector3.one;

        [SerializeField, Tooltip("How the respawn is generated")]
        private RespawnType respawnType = RespawnType.Collect;

        [SerializeField, Tooltip("Reference to the ground layer mask to spawn the shard on")]
        private LayerMask groundLayerMask;
        

        private void Start()
        {
            TriggerShardSpawn();
        }

        /// <summary>
        /// Begins the co routine for spawn shards
        /// </summary>
        public void TriggerShardSpawn()
        {
            StartCoroutine(SpawnShardRoutine(Random.Range(minSpawnTime, maxSpawnTime)));
        }

        
        /// <summary>
        /// Routine to countdown to shard spawn
        /// </summary>
        /// <param name="spawnTime"></param>
        /// <returns></returns>
        private IEnumerator SpawnShardRoutine(float spawnTime)
        {
            yield return new WaitForSeconds(spawnTime);
            OnSpawnShard();
        }

        /// <summary>
        /// Called when a shard is ready to spawn
        /// </summary>
        private void OnSpawnShard()
        {
            if (shardPrefab == null)
            {
                Debug.LogError($"Cannot spawn shard as prefab not assigned");
                return;
            }


            if (spawnOnTransform != null)
            {
                GameObject instance = GameObject.Instantiate(shardPrefab, spawnOnTransform);
            }

            if (respawnType == RespawnType.Loop)
                TriggerShardSpawn();
        }

        /// <summary>
        /// Gets random point inside the bounds to spawn the object
        /// </summary>
        /// <returns>The position to spawn the shard at</returns>
        private Vector3 GetRandomSpawnPoint()
        {
            int maxLoop = 30;
            for (var i = 0; i < maxLoop; ++i)
            {
                x = transform.position.x + (Random.Range(-spawnBoxBounds.x / 2, spawnBoxBounds.x / 2)),
                y = 20f,
                z = transform.position.z + (Random.Range(-(spawnBoxBounds.z / 2), (spawnBoxBounds.z / 2)))
            };

            if (Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hit, 30f, groundLayerMask))
            {
                targetPosition = hit.point;
                Vector3 targetPosition =  new Vector3
                {
                    x = transform.position.x + (Random.Range(-spawnBoxBounds.x / 2, spawnBoxBounds.x / 2)),
                    y = 20f,
                    z = transform.position.z + (Random.Range(-(spawnBoxBounds.z / 2), (spawnBoxBounds.z / 2)))
                };

                if (Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hit, 30f, groundLayerMask))
                {
                    targetPosition = hit.point;
                }

                if (!ShardAtSpawnPosition(targetPosition))
                    return targetPosition;
            }
            
            #if UNITY_EDITOR
            Debug.LogWarning("Couldn't find a empty place to spawn shard after 30 loops, skipping spawn position");
            #endif
            return Vector3.zero;
        }

            return targetPosition;
        }
        
        #if UNITY_EDITOR
        public void OnDrawGizmosSelected()
        {
            if (spawnBoxBounds == Vector3.zero)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, spawnBoxBounds);
        }
#endif
    }
}