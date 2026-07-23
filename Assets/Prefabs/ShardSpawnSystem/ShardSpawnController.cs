using System.Collections.Generic;
using System.Collections;
using UnityEngine;

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
    }
}