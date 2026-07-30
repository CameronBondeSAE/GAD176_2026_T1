using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace Sabre.AI
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private int maxCharacters;
        [SerializeField] private GameObject charPrefab;
        public GameObject preTarget;
        [SerializeField] private float variationRange = 5;
        public List<GameObject> playerList = new List<GameObject>();
        public TextMeshProUGUI text;
        

        private void Start()
        {
            if(charPrefab != null)
            {
                Spawn(maxCharacters);
            }
            UpdateText();
        }

        private void Spawn(int AmountSpawned)
        {
            for (int i = 0; i < AmountSpawned; i++)
            {
                float randX = Random.Range(-variationRange, variationRange);
                float randZ = Random.Range(-variationRange, variationRange);
                float randQ = Random.Range(-180f, 180f);

                GameObject SpawnedChar = Instantiate(charPrefab, transform.position + new Vector3(randX, 0, randZ), Quaternion.Euler(0, randQ, 0));
                playerList.Add(SpawnedChar);
                
            }
            UpdateText();
        }

        public void IncreasePlayerCount(int Change)
        {
            maxCharacters += Change;
            Spawn(Change);
            
        }

        public void DecreasePlayerCount(int change)
        {
            
            maxCharacters -= change;
            if(maxCharacters < 0)
            {
                maxCharacters = 0;
                return;
            }

            for(int i = 0; i < change; i ++)
            {
                GameObject CulledPlayer = playerList[playerList.Count - 1];
                playerList.Remove(CulledPlayer);
                Destroy(CulledPlayer);
            }
            UpdateText();
        }

        private void UpdateText()
        {
            text.text = "NPC Count: " + maxCharacters;
        }
    }
}