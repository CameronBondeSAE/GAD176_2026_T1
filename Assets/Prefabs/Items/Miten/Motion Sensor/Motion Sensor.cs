using System.Collections;
using System.Collections.Generic;
using Divij;
using Frank;
using Keegan.FOV;
using UnityEngine;

public class MotionSensor : MonoBehaviour, IPowered, IPickup
{
    public int neededEnergy;
    public int totalEnergy;
    public int activationEnergy;
    public bool enoughEnergy;
    public bool continuespawning;
    public bool portalFXRunning;
    public GameObject FOV;

    public List<Shard_Model> shardList = new List<Shard_Model>();
    
    public void SetPowered(bool powered)
    {
        throw new System.NotImplementedException();
    }

    public bool GetPowered()
    {
        throw new System.NotImplementedException();
    }
    
    
    public void ReceivePotentialEnergy(Shard_Model shard)
    {
        shardList.Add(shard);
    }
	
    public void PotentialEnergyRemoved(Shard_Model shard)
    {
        shardList.Remove(shard);
    }

    void Start()
    {
        FOV = GameObject.Find("FOVDetection Motion Sensor");
        StartCoroutine(CalcLoop());
        StartCoroutine(PortalOn());
        activationEnergy = 2;
        neededEnergy = 2;
    }
    
    bool CalculateNeededEnergy()
    {
        // Pass 1: Count total energy
        totalEnergy = 0;
        foreach (Shard_Model shard in shardList)
        {
            totalEnergy += shard.CurrentEnergy;
        }

        if (totalEnergy >= activationEnergy)
        {
            return enoughEnergy = true;

        }else
        {

        }
        
        if (enoughEnergy)
        { 
            FOV.SetActive(true);
        }
 
        return enoughEnergy = false;
    }

    bool CalculateContinueEnergy()
    {
        foreach (Shard_Model shard in shardList)
        {
            totalEnergy = 0;
            totalEnergy += shard.CurrentEnergy;
        }

        if (totalEnergy >= neededEnergy)
        {
            return continuespawning = true;
        }
        else
        {
            return continuespawning = false;
        }
    }



    public IEnumerator PortalOn()
    {
        if (enoughEnergy == true)
        {
            while (continuespawning == true)
            {
                yield return new WaitForSeconds(1);
                int remainingToRemove = totalEnergy;
                int neededToCont = 2;

                // Pass 2: Remove energy
                foreach (Shard_Model shard in shardList)
                {
                    if (remainingToRemove <= 0)
                        break;

                    if (shard.CurrentEnergy < neededToCont)
                    {
                        if (shard.CurrentEnergy <= remainingToRemove)
                        {
                            shard.UseEnergy(shard.CurrentEnergy);
                            shardList.Remove(shard);
                            Destroy(shard.gameObject);
                        }
                        else
                        {
                            shard.UseEnergy(remainingToRemove);
                            remainingToRemove = 0;
                        }
                    } else if (shard.CurrentEnergy >= neededToCont)
                    {
                        remainingToRemove = neededToCont;
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

                CalculateContinueEnergy();
            }
            if (continuespawning == false)
            {
                FOV.SetActive(false);
            }
        }
    }

    public IEnumerator CalcLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            CalculateNeededEnergy();
        }
    }
}
