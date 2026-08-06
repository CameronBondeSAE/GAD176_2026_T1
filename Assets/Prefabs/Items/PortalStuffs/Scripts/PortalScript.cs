using System.Collections;
using System.Collections.Generic;
using Divij;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering.HighDefinition;

public class PortalScript : MonoBehaviour, IPowered
{
    public List<GameObject> lightnings = new List<GameObject>();

    public int neededEnergy;
    public int totalEnergy;
    public int activationEnergy;
    public bool enoughEnergy;
    public bool continuespawning;
    public bool portalFXRunning;

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
        StartCoroutine(PortalOn());
    }
    
    bool CalculateNeededEnergy()
    {
        // Pass 1: Count total energy
        foreach (Shard_Model shard in shardList)
        {
            totalEnergy = 0;
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
            if (!portalFXRunning)
                StartCoroutine(portalActivated());
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
                if (!portalFXRunning)
                {
                    portalFXRunning = true;
                    StartCoroutine(portalActivated());
                }
                
                yield return new WaitForSeconds(1);
                int remainingToRemove = totalEnergy;
                int neededToSpawn = 10;

                // Pass 2: Remove energy
                foreach (Shard_Model shard in shardList)
                {
                    if (remainingToRemove <= 0)
                        break;

                    if (shard.CurrentEnergy < neededToSpawn)
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
                    } else if (shard.CurrentEnergy >= neededToSpawn)
                    {
                        remainingToRemove = neededToSpawn;
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
                portalFXRunning = false;
                StopCoroutine(portalActivated());
            }
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

    IEnumerator portalActivated()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject chosenlightning = lightnings[Random.Range(0, lightnings.Count)];
        chosenlightning.gameObject.SetActive(!chosenlightning.gameObject.activeSelf);
    }
}
