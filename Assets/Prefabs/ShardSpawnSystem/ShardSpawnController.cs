using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Keegan.ShardSpawn
{
    public class ShardSpawnController : MonoBehaviour
    {
        public enum RespawnType
        {
            None,
            Collected,
            Loop
        }
        
        [SerializeField, Tooltip("The prefab for the shard that will spawn")]
        private GameObject shardPrefab;

        [SerializeField, Tooltip("True if the shard can be used as it's inside the time range")]
        private bool canSpawnShard = false;

        [Header("Spawn Time Settings")]
        [SerializeField, Tooltip("The min amount of time before a shard will spawn (again)")]
        private float minSpawnTime = 20f;

        [SerializeField, Tooltip("The max amount of time before a shard will spawn (again")]
        private float maxSpawnTime = 100f;

        [SerializeField, Tooltip("True if the spawner only functions at specific times")]
        private bool spawnDuringTimeRange;

        [SerializeField, Tooltip("The hour that the shards will spawn")]
        private int shardSpawnFrom;

        [SerializeField, Tooltip("The hour that the shards stop spawning")]
        private int shardSpawnTo;
        
        [SerializeField, Tooltip("Reference to the transform that the shard will spawn on")]
        private Transform spawnOnTransform;

        [SerializeField, Tooltip("Bounds for the range to spawn a shard in")]
        private Vector3 spawnBoxBounds = Vector3.one;

        [SerializeField, Tooltip("The layer to check when locating the ground")]
        private LayerMask groundLayerMask;

        [SerializeField, Tooltip("The layer to check for shards on to prevent spawning on top")]
        private LayerMask obstacleLayerMask;

        [SerializeField, Tooltip("The method used to respawn shards once spawned")]
        private RespawnType respawnType;

        // TODO: Replace GameObject with ShardController
        // Reference to all the spawned shards in this spawner
        private List<GameObject> spawnedShards = new List<GameObject>();

        private void Start()
        {
            TriggerShardSpawn();
        }

        private void OnEnable()
        {
            // TODO: Add listener to the sun controllers hour event
        }

        private void OnDisable()
        {
            // TODO: Remove listener from the sun controller hour event
        }

        /// <summary>
        /// Begins the co routine for spawn shards
        /// </summary>
        public void TriggerShardSpawn()
        {
            if(canSpawnShard)
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
                instance.transform.position = spawnOnTransform.position;
                // TODO: Get the shard controller 
                // TODO: Subscribe to the health system
            }

            if (respawnType == RespawnType.Loop)
                TriggerShardSpawn();
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