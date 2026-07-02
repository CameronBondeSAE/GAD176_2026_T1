using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;



    public class HealthSys : MonoBehaviour, IDepletableBars
    {
        public int healthMax = 100;
        public int healthCurrent;
        public int healthMin = 0;
        public int healthNegative;
        public Rigidbody attachedEntity;
        public DepleteUI depleteUI;

        public void Start()
        {
            healthCurrent = healthMax;
            depleteUI.DisplayInitialise();
            attachedEntity = GetComponent<Rigidbody>();

        }
        
        [FormerlySerializedAs("Damaged")] public UnityEvent OnDamagedEvent =  new UnityEvent();

        [FormerlySerializedAs("OnHealthDepletion")] public UnityEvent OnDeathEvent = new UnityEvent();

        private void OnEnable()
        {
            OnDeathEvent.AddListener(depleteUI.DisplayDeathMessage);
            OnDamagedEvent.AddListener(depleteUI.DisplayHealthValue);
            
        }

        private void OnDisable()
        {
            OnDeathEvent.RemoveListener(depleteUI.DisplayDeathMessage);
            OnDamagedEvent.RemoveListener(depleteUI.DisplayHealthValue);
        }
        
        public int MaxValue()
        {
            return healthMax;
        }

        public int MinValue()
        {
            return healthMax - healthMax;
        }

        public int CurrentValue()
        {
            return healthCurrent;
        }

        public void OnDmg(int healthNegative)
        {
            Debug.Log("You are taking " + healthNegative);
            
            if (healthCurrent == healthMax)
            {
                healthCurrent = healthMax;
                healthCurrent = healthCurrent - healthNegative;
                OnDamagedEvent.Invoke();
                if (healthCurrent <= healthMin)
                {
                    OnDeathEvent.Invoke();
                    Debug.Log("You Dead!");
                }
            }
            else
            {
                healthCurrent = healthCurrent - healthNegative;
                OnDamagedEvent.Invoke();
                if (healthCurrent <= healthMin)
                {
                    OnDeathEvent.Invoke();
                    Debug.Log("You Dead!");
                }
            }

            ;
        }

    }

