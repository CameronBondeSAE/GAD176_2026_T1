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

        [SerializeField, Tooltip("The hour that the shards will spawn"), Range(0, 24)]
        private int shardSpawnFrom;

        [SerializeField, Tooltip("The hour that the shards stop spawning"), Range(0, 24)]
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
        private List<TestShard> spawnedShards = new List<TestShard>();

        private SuntoTime sunTime;

        private void Start()
        {
            TriggerShardSpawn();
        }

        private void OnEnable()
        {
            if (sunTime == null)
                sunTime = GameObject.FindFirstObjectByType<SuntoTime>();

            if (sunTime != null)
            {
                sunTime.OnHourChange.AddListener(OnHourChange);
            }
        }

        private void OnDisable()
        {
            if(sunTime != null)
                sunTime.OnHourChange.RemoveListener(OnHourChange);
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
                Vector3 targetPosition = GetRandomSpawnPoint(instance.GetComponentInChildren<Collider>().bounds.extents);
                if (targetPosition != Vector3.zero)
                {
                    instance.transform.position = targetPosition;
                    TestShard shard = instance.GetComponentInChildren<TestShard>();
                    if (shard != null)
                    {
                        spawnedShards.Add(shard);
                        shard.OnShardCollected.AddListener(OnShardCollected);
                    }
                    else
                    {
                        Destroy(instance);
                    }
                }
                else
                {
                    Destroy(instance);
                }
            }

            if (respawnType == RespawnType.Loop)
                TriggerShardSpawn();
        }

        /// <summary>
        /// Gets random point inside the bounds to spawn the object
        /// </summary>
        /// <param name="shardBoundsExtent">The collision bounds of the shard</param>
        /// <returns>The position to spawn the shard at</returns>
        private Vector3 GetRandomSpawnPoint(Vector3 shardBoundsExtent)
        {
            int maxLoop = 30;
            for (var i = 0; i < maxLoop; ++i)
            {
                Vector3 targetPosition =  new Vector3
                {
                    x = transform.position.x + (Random.Range(-spawnBoxBounds.x / 2, spawnBoxBounds.x / 2)),
                    y = 6f,
                    z = transform.position.z + (Random.Range(-(spawnBoxBounds.z / 2), (spawnBoxBounds.z / 2)))
                };

                if (Physics.Raycast(targetPosition, Vector3.down, out RaycastHit hit, 30f, groundLayerMask))
                {
                    targetPosition = hit.point;
                }

                if (!ShardAtSpawnPosition(new Vector3(targetPosition.x, (targetPosition.y - shardBoundsExtent.y), targetPosition.z), shardBoundsExtent))
                    return targetPosition;
            }
            
            #if UNITY_EDITOR
            Debug.LogWarning("Couldn't find a empty place to spawn shard after 30 loops, skipping spawn position");
            #endif
            return Vector3.zero;
        }

        public bool ShardAtSpawnPosition(Vector3 position, Vector3 boundsExtent)
        {
            return Physics.CheckBox(position, boundsExtent, Quaternion.identity, obstacleLayerMask);
        }

        /// <summary>
        /// Invoked when a shard has been collected
        /// TODO: Replace GameObject with the ShardController
        /// </summary>
        /// <param name="collected">Reference to the shard that was collected</param>
        public void OnShardCollected(TestShard collected)
        {
            if (spawnedShards.Contains(collected))
            {
                collected.OnShardCollected.RemoveListener(OnShardCollected);
                spawnedShards.Remove(collected);
                
                if (respawnType == RespawnType.Collected)
                {
                    TriggerShardSpawn();
                }
            }
        }

        /// <summary>
        /// Checks when the hour changes if the shard spawn should be enabled
        /// </summary>
        /// <param name="hour">The current hour emitted</param>
        public void OnHourChange()
        {
            if (sunTime == null)
                return;

            // Get the 24 hour time
            int currentHour = sunTime.modhours + (sunTime.AMPM ? 0 : 12);

            if (currentHour >= shardSpawnFrom && currentHour <= shardSpawnTo)
            {
                if (!canSpawnShard)
                {
                    canSpawnShard = true;
                    TriggerShardSpawn();
                }
            }
            else
            {
                if(canSpawnShard)
                    canSpawnShard = false;
            }
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