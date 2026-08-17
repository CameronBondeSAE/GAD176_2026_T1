using System.Collections;
using System.Collections.Generic;
using Divij;
using UnityEngine;

public class PortalScript : MonoBehaviour, IPowered
{
    public List<GameObject> lightnings = new List<GameObject>();

    public int neededEnergy = 100;
    public int totalEnergy;
    public int activationEnergy = 700;

    public bool enoughEnergy;
    public bool continuespawning;
    public bool portalFXRunning;

    public List<GameObject> Aliens = new List<GameObject>();
    public GameObject pspawnPoint;

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
        if (!shardList.Contains(shard))
        {
            shardList.Add(shard);
        }
    }

    public void PotentialEnergyRemoved(Shard_Model shard)
    {
        shardList.Remove(shard);
    }

    private void Start()
    {
        StartCoroutine(PortalLoop());
    }

    private void CalculateTotalEnergy()
    {
        totalEnergy = 0;

        for (int i = shardList.Count - 1; i >= 0; i--)
        {
            if (shardList[i] == null)
            {
                shardList.RemoveAt(i);
                continue;
            }

            totalEnergy += shardList[i].CurrentEnergy;
        }
    }

    private IEnumerator PortalLoop()
    {
        while (true)
        {
            CalculateTotalEnergy();

            enoughEnergy = totalEnergy >= activationEnergy;

            if (enoughEnergy)
            {
                if (!portalFXRunning)
                {
                    portalFXRunning = true;
                    ActivatePortalFX();
                }

                continuespawning = totalEnergy >= neededEnergy;

                if (continuespawning)
                {
                    UseEnergy(neededEnergy);

                    SpawnAlien();
                }
            }
            else
            {
                continuespawning = false;
                portalFXRunning = false;

                DisablePortalFX();
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void UseEnergy(int amount)
    {
        int remaining = amount;

        for (int i = shardList.Count - 1; i >= 0; i--)
        {
            if (remaining <= 0)
                break;

            Shard_Model shard = shardList[i];

            if (shard == null)
            {
                shardList.RemoveAt(i);
                continue;
            }

            int amountToUse = Mathf.Min(
                shard.CurrentEnergy,
                remaining
            );

            shard.UseEnergy(amountToUse);
            remaining -= amountToUse;

            if (shard.CurrentEnergy <= 0)
            {
                shardList.RemoveAt(i);
                Destroy(shard.gameObject);
            }
        }
    }

    private void SpawnAlien()
    {
        if (Aliens.Count == 0 || pspawnPoint == null)
            return;

        GameObject chosenAlien =
            Aliens[Random.Range(0, Aliens.Count)];

        Instantiate(
            chosenAlien,
            pspawnPoint.transform.position,
            pspawnPoint.transform.rotation
        );
    }

    private void ActivatePortalFX()
    {
        if (lightnings.Count == 0)
            return;

        GameObject chosenLightning =
            lightnings[Random.Range(0, lightnings.Count)];

        chosenLightning.SetActive(true);
    }

    private void DisablePortalFX()
    {
        foreach (GameObject lightning in lightnings)
        {
            if (lightning != null)
            {
                lightning.SetActive(false);
            }
        }
    }
}