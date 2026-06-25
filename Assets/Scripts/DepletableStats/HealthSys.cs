using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthSys : MonoBehaviour, IDepletableBars
{
    public int healthMax;
    public int healthCurrent;
    public int healthMin;
    public int healthNegative;
    public Canvas canvas;
    public Slider healthDisplay;
    public Rigidbody attachedEntity;
    
    public void Start()
    {
        attachedEntity = GetComponent<Rigidbody>();
        //if
    }

    public UnityEvent OnDepletion = new UnityEvent();

    private void OnEnable()
    {
        //OnDepletion.AddListener(e.g Player.OnDeath() );
    }

    private void OnDisable()
    {
        //OnDepletion.RemoveListener(e.g Player.OnDeath() );
    }

    public void UiDisplayUpdate()
    {
        
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
    
    private void OnDmg()
    {
        if(healthCurrent == healthMax)
        {
            healthCurrent = healthMax;
            healthCurrent = healthCurrent - healthNegative;
            if (healthCurrent <= 0)
            {
                OnDepletion.Invoke();
            }
            
        }
        else
        {
            healthCurrent = healthCurrent - healthNegative;
            if (healthCurrent <= 0)
            {
                OnDepletion.Invoke();
            }
        };
    }

}
