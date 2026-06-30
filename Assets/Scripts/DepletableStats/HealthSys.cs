using UnityEngine;
using UnityEngine.Events;
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
        
        public UnityEvent Damaged =  new UnityEvent();

        public UnityEvent OnHealthDepletion = new UnityEvent();

        private void OnEnable()
        {
            OnHealthDepletion.AddListener(depleteUI.DisplayDeathMessage);
            Damaged.AddListener(depleteUI.DisplayHealthValue);
            
        }

        private void OnDisable()
        {
            OnHealthDepletion.RemoveListener(depleteUI.DisplayDeathMessage);
            Damaged.RemoveListener(depleteUI.DisplayHealthValue);
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
                Damaged.Invoke();
                if (healthCurrent <= healthMin)
                {
                    OnHealthDepletion.Invoke();
                    Debug.Log("You Dead!");
                }
            }
            else
            {
                healthCurrent = healthCurrent - healthNegative;
                Damaged.Invoke();
                if (healthCurrent <= healthMin)
                {
                    OnHealthDepletion.Invoke();
                    Debug.Log("You Dead!");
                }
            }

            ;
        }

    }

