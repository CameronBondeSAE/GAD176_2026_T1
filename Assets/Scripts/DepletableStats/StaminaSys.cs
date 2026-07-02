using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class StaminaSys : MonoBehaviour, IDepletableBars
{
    public int staminaMax;
    public int staminaCurrent;
    public int staminaMin;
    public int staminaNegative;
    public Slider staminaDisplay;
    public Rigidbody attachedEntity;

    public void Start()
    {
        attachedEntity = GetComponent<Rigidbody>();
    }

    [FormerlySerializedAs("OnStaminaDepletion")] public UnityEvent StaminaDepletedEvent = new UnityEvent();

    private void OnEnable()
    {
        //OnDepletion.AddListener(e.g Player.sprint() );
    }

    private void OnDisable()
    {
        //OnDepletion.RemoveListener(e.g Player.sprint() );
    }

    public void UiDisplayUpdate()
    {
        
    }

    public int MaxValue()
    {
        return staminaMax;
    }

    public int MinValue()
    {
        return staminaMax - staminaMax;
    }

    public int CurrentValue()
    {
        return staminaCurrent;
    }

    private void OnUse()
    {
        if(staminaCurrent == staminaMax)
        {
            staminaCurrent = staminaMax;
            staminaCurrent = staminaCurrent - staminaNegative;
        }
        else
        {
            staminaCurrent = staminaCurrent - staminaNegative;
            if (staminaCurrent <= staminaMin)
            {
                StaminaDepletedEvent.Invoke();
            }
        };
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
