using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Divij
{
    public class SwitchableLightModel : NetworkBehaviour, IPowered
    {
        public NetworkVariable<bool> isPowered;

        public NetworkVariable<bool> isSwitchedOn = new(true);

        public bool debugRandomSwitching = false;

        public int requiredEnergyForOneSecond = 1;

        [SerializeField]
        private Shard_Model currentNearByShard;
        
        public HashSet<Shard_Model> shards;
        public int shardsTotalNearBy = 0;

        private void Awake()
        {
	        shards = new HashSet<Shard_Model>();
        }

        private void FixedUpdate()
        {
	        if (debugRandomSwitching && Random.value < 0.003f)
	        {
		        SetPowered(true);
		        ToggleSwitch();
	        }
        }

        public void ToggleSwitch()
        {
	        if (IsServer)
	        {
		        isSwitchedOn.Value = !isSwitchedOn.Value;
		        // Debug.Log("CLICK: SwitchableLight: Toggled = "+isSwitchedOn);
	        }
        }


        public void SetPowered(bool powered)
        {
	        if (IsServer)
	        {
		        isPowered.Value = powered;
	        }
        }

        public bool GetPowered()
        {
	        return isPowered.Value;
        }
        
        // TODO: Support multiple nearby shards
        Coroutine coroutine;
        public void ReceivePotentialEnergy(Shard_Model shard)
        {
	        if (IsServer)
	        {
		        shards.Add(shard);
		        shardsTotalNearBy = shards.Count;
		        coroutine = StartCoroutine(CheckToLightUpForOneSecond());
	        }
        }

        public void PotentialEnergyRemoved(Shard_Model shard)
        {
	        if (IsServer)
	        {
		        shards.Remove(shard);
		        shardsTotalNearBy = shards.Count;
		        // StopCoroutine(coroutine);
	        }
        }

        private IEnumerator CheckToLightUpForOneSecond()
        {
	        int totalEnergyNearBy = 0;
	        do
	        {
		        totalEnergyNearBy = 0;

		        // Before
		        foreach (Shard_Model shard in shards)
		        {
			        totalEnergyNearBy += shard.CurrentEnergy;
		        }

		        int tempEnergyUsedForMultipleShards = 0;
		        foreach (Shard_Model shard in shards)
		        {
			        // There's enough energy in on shard here
			        if (shard.UseEnergy(requiredEnergyForOneSecond - tempEnergyUsedForMultipleShards)) // TODO What if the previous shard was half the requirment, this would use up ALL PLUS the previous bit
			        {
				        tempEnergyUsedForMultipleShards += requiredEnergyForOneSecond - tempEnergyUsedForMultipleShards;
			        }
			        else
			        {
				        // Use up any remainder
				        tempEnergyUsedForMultipleShards += shard.CurrentEnergy;
				        shard.UseEnergy(shard.CurrentEnergy);
			        }

			        // We've gathered enough energy from one or more shards. Turn on
			        // Debug.Log(tempEnergyUsedForMultipleShards);
			        if (tempEnergyUsedForMultipleShards >= requiredEnergyForOneSecond)
			        {
				        SetPowered(true);
				        break;
			        }
		        }

		        yield return new WaitForSeconds(1);

		        // After
		        totalEnergyNearBy = 0;
		        foreach (Shard_Model shard in shards)
		        {
			        totalEnergyNearBy += shard.CurrentEnergy;
		        }
	        } while (totalEnergyNearBy >= requiredEnergyForOneSecond);
	        SetPowered(false);
        }
    }
}


