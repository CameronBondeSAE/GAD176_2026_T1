using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Keegan.ShardSpawn
{
    public class ShardSpawnController : MonoBehaviour
    {
        [SerializeField, Tooltip("The prefab for the shard that will spawn")]
        private GameObject shardPrefab;

        [Header("Spawn Time Settings")]
        [SerializeField, Tooltip("The min amount of time before a shard will spawn (again)")]
        private float minSpawnTime = 20f;

        [SerializeField, Tooltip("The max amount of time before a shard will spawn (again")]
        private float maxSpawnTime = 100f;

        [SerializeField, Tooltip("True if the shards will spawn on a loop")]
        private bool loopSpawn = true;
        
        [SerializeField, Tooltip("Reference to the transform that the shard will spawn on")]
        private Transform spawnOnTransform;

        [SerializeField, Tooltip("Bounds for the range to spawn a shard in")]
        private Vector3 spawnBoxBounds = Vector3.one;
        

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
                instance.transform.position = spawnOnTransform.position;
            }

            if (loopSpawn)
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