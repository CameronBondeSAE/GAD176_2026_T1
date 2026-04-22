using Anthill.AI;
using Team_Members.Jackson.AI_Senses.Hearing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team_Members.Jackson.AI_Senses
{
    public class EnemySense : MonoBehaviour, ISense
    {
        [SerializeField] private AudioSource audioSource;
        private SoundEmitter _soundEmitter;
        public bool seePlayer = false;
        public bool touchedPlayer = false;
        public GameObject UIOverlay;
        public GameObject gameOverScreen;
    
        private void Awake()
        {
            _soundEmitter = GetComponent<SoundEmitter>();
        }
    
        private void Update()
        {
            if (Random.Range(0, 100) == 0)
            {
                _soundEmitter.Emit(SoundTypes.Roaring);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                touchedPlayer = true;
                Destroy(other.gameObject);
            }
        }

        public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
        {
            aWorldState.Set(Enemy.SeePlayer, seePlayer);
            aWorldState.Set(Enemy.TouchedPlayer, touchedPlayer);
        }
    
    }
}
