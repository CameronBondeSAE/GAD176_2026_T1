using System.Collections.Generic;
using UnityEngine;


namespace Keegan.Shard
{
    public class ShardCarrier : MonoBehaviour
    {
        [SerializeField, Tooltip("Reference to the transform that the shard will attach to when picked up")]
        private Transform carryTransform;
        // Reference to the shard that is being carried
        private Shard_Model carryingShard;
    }
}