using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Divij
{
    public class SwitchableLight : MonoBehaviour, IInteractable, IPowered
    {
        public Light light;
        
        public bool isPowered;

        public bool isSwitchedOn = true;

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

        private void Start()
        {
	        CheckPower();
        }

        private void FixedUpdate()
        {
	        if (debugRandomSwitching && Random.value < 0.003f)
	        {
		        SetPowered(true);
		        ToggleSwitch();
	        }
        }

        // This is the interface entry point
        public void Interact()
        {
            ToggleSwitch();
        }

        public void ToggleSwitch()
        {
	        isSwitchedOn = !isSwitchedOn;
	        // Debug.Log("CLICK: SwitchableLight: Toggled = "+isSwitchedOn);

	        CheckPower();
        }

        public void CheckPower()
        {
	        if (light == null)
	        {
		        Debug.LogWarning("Light needs to be assigned");
		        return;
	        }
	        
	        if (isPowered && isSwitchedOn)
		        light.enabled = true;
	        else
		        light.enabled = false;
        }


        public void SetPowered(bool powered)
        {
            isPowered = powered;

            CheckPower();
        }

        public bool GetPowered()
        {
	        return isPowered;
        }
        
        
        // TODO: Support multiple nearby shards
        Coroutine coroutine;
        public void ReceivePotentialEnergy(Shard_Model shard)
        {
	        shards.Add(shard);
	        shardsTotalNearBy = shards.Count;
	        coroutine = StartCoroutine(CheckToLightUpForOneSecond());
        }

        public void PotentialEnergyRemoved(Shard_Model shard)
        {
	        shards.Remove(shard);
	        shardsTotalNearBy = shards.Count;
			// StopCoroutine(coroutine);
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


