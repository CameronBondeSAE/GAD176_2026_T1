using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Keegan.ShardSpawn
{
    public class TestShard : MonoBehaviour
    {
        public UnityEvent<TestShard> OnShardCollected;
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponentInChildren<Player_Controller>() != null)
            {
                OnShardCollected.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}