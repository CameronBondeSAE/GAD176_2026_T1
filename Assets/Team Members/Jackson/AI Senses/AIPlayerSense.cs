using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using Team_Members.Jackson.AI_Senses.Hearing;
using UnityEngine;

namespace Team_Members.Jackson.AI_Senses
{
    public class AIPlayerSense : MonoBehaviour, ISense
    {
        private PlayerStorage _playerStorage;
        [SerializeField] private GameObject exclamationMark;
        [SerializeField] private EnemySense[] enemies;
        public List<GameObject> inactivePowerUps;
        public bool foundDispenser = false;
        public bool playerWorking = false;
        public bool missingPowerUp = false;
        public bool playerHasPowerUp = false;
        public bool foundInactivePowerUp = false;
        public bool powerUpDelivered = false;
        public int pointsEarned = 0;
        public Transform dispenserTransform;
        public Transform inactivePowerUpTransform;
        public GameObject backpack;
        public AudioSource audioSource;
        public AudioClip boxCollectedClip;
        public AudioClip boxDeliveredClip;
    
        private void Awake()
        {
            _playerStorage = GetComponent<PlayerStorage>();
            enemies = FindObjectsByType<EnemySense>(FindObjectsSortMode.None);
        }

        private void OnEnable()
        {
            SoundReceiver.ScaredSoundEvent.AddListener(GotScared);
        
        }

        private void OnDisable()
        {
            SoundReceiver.ScaredSoundEvent.RemoveListener(GotScared);
        }
    
        private void GotScared()
        {
            Debug.Log("Heard Sound");
            StartCoroutine(Scared());
        }
    
        private IEnumerator Scared()
        {
            foreach (EnemySense enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= 10)
                {
                    exclamationMark.SetActive(true);
                }
                else if (Vector3.Distance(transform.position, enemy.transform.position) > 10)
                {
                    exclamationMark.SetActive(false);
                }

                yield return null;
            }
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.Set(AIPlayer.PlayerWorking, playerWorking);
            aWorldState.Set(AIPlayer.MissingPowerUp, missingPowerUp);
            aWorldState.Set(AIPlayer.FoundDispenser, foundDispenser);
            aWorldState.Set(AIPlayer.PlayerHasPowerUp, playerHasPowerUp);
            aWorldState.Set(AIPlayer.FoundInactivePowerUp, foundInactivePowerUp);
            aWorldState.Set(AIPlayer.PowerUpDelivered, powerUpDelivered);
        }
    }
}
