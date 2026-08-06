using System.Collections;
using System.Collections.Generic;
using Divij;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PortalScript : MonoBehaviour, IPowered
{
    public GameObject lightning1;
    public GameObject lightning2;
    public GameObject lightning3;
    public GameObject lightning4;
    public GameObject lightning5;
    public GameObject lightning6;
    public GameObject lightning7;

    public int neededEnergy;
    public int totalEnergy;

    public List<Shard_Model> shardList = new List<Shard_Model>();

    public void SetPowered(bool powered)
    {
        throw new System.NotImplementedException();
    }

    public bool GetPowered()
    {
        throw new System.NotImplementedException();
    }

    void ReceivePotentialEnergy(Shard_Model shard)
    {
        shardList.Add(shard);
    }
	
    void PotentialEnergyRemoved(Shard_Model shard)
    {
        shardList.Remove(shard);
    }

    void Start()
    {
        StartCoroutine(calcloop());
    }
    
    void CalculateNeededEnergy()
    {
        // Pass 1: Count total energy
        foreach (Shard_Model shard in shardList)
        {
            totalEnergy += shard.CurrentEnergy;
        }

        if (totalEnergy >= neededEnergy)
        {
            int remainingToRemove = neededEnergy;

            // Pass 2: Remove energy
            foreach (Shard_Model shard in shardList)
            {
                if (remainingToRemove <= 0)
                    break;

                if (shard.CurrentEnergy <= remainingToRemove)
                {
                    shard.UseEnergy(shard.CurrentEnergy);
                    Destroy(shard.gameObject);
                }
                else
                {
                    shard.UseEnergy(remainingToRemove);
                    remainingToRemove = 0;
                }
            }
        }
        else
        {
            totalEnergy = 0;
        }
    }

    public IEnumerator calcloop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CalculateNeededEnergy();
        }
    }
    
}
