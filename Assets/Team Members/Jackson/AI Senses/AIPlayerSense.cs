using System.Collections;
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
        public bool boxSpawned = false;
        public bool foundBox = false;
        public bool playerWorking = false;
        public bool playerHasBox = false;
        public bool foundCollector = false;
        public bool boxDelivered = false;
        public int pointsEarned = 0;
        public Transform boxTransform;
        public Transform boxCollectorTransform;
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
            // Temporarily making it true before I setup the Box Spawning
            aWorldState.Set(AIPlayer.BoxSpawned, boxSpawned);
            aWorldState.Set(AIPlayer.PlayerWorking, playerWorking);
            aWorldState.Set(AIPlayer.FoundBox, foundBox);
            aWorldState.Set(AIPlayer.PlayerHasBox, playerHasBox);
            aWorldState.Set(AIPlayer.FoundCollector, foundCollector);
            aWorldState.Set(AIPlayer.BoxDelivered, boxDelivered);
        }
    }
}
