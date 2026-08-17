using System.Collections;
using System.Collections.Generic;
using Divij;
using Frank;
using Keegan.FOV;
using UnityEngine;

public class trto : MonoBehaviour, IPowered, IPickup
{
    public int activationEnergy = 2;
    public int neededEnergy = 2;
    public int totalEnergy;

    public bool enoughEnergy;
    public bool continuespawning;

    public GameObject FOV;

    public HashSet<Shard_Model> shardList = new HashSet<Shard_Model>();

    private void Start()
    {
        FOV.SetActive(false);

        StartCoroutine(EnergyLoop());
    }
    

    public void ReceivePotentialEnergy(Shard_Model shard)
    {
        shardList.Add(shard);
    }

    public void PotentialEnergyRemoved(Shard_Model shard)
    {
        shardList.Remove(shard);
    }

    private void CalculateTotalEnergy()
    {
        totalEnergy = 0;

        foreach (Shard_Model shard in shardList)
        {
            if (shard != null)
            {
                totalEnergy += shard.CurrentEnergy;
            }
        }
    }

    private IEnumerator EnergyLoop()
    {
        while (true)
        {
            CalculateTotalEnergy();

            enoughEnergy = totalEnergy >= activationEnergy;

            if (enoughEnergy)
            {
                FOV.SetActive(true);

                // Consume energy every second
                UseEnergy(neededEnergy);
            }
            else
            {
                FOV.SetActive(false);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void UseEnergy(int amountNeeded)
    {
        int remaining = amountNeeded;

        // Make a copy because we might remove shards
        List<Shard_Model> shardsToCheck =
            new List<Shard_Model>(shardList);

        foreach (Shard_Model shard in shardsToCheck)
        {
            if (shard == null)
                continue;

            if (remaining <= 0)
                break;

            int energyTaken = Mathf.Min(
                shard.CurrentEnergy,
                remaining
            );

            shard.UseEnergy(energyTaken);

            remaining -= energyTaken;

            if (shard.CurrentEnergy <= 0)
            {
                shardList.Remove(shard);
                Destroy(shard.gameObject);
            }
        }

        CalculateTotalEnergy();

        continuespawning = totalEnergy >= neededEnergy;

        if (!continuespawning)
        {
            FOV.SetActive(false);
        }
    }
    
    public void SetPowered(bool powered)
    {
        throw new System.NotImplementedException();
    }

    public bool GetPowered()
    {
        throw new System.NotImplementedException();
    }
}