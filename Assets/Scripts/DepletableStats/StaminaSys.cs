using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class StaminaSys : MonoBehaviour, IDepletableBars
{
    public int staminaMax = 100;
    public int staminaCurrent;
    public int staminaMin = 0;
    public int staminaUsage;
    public DepleteUI depleteUI;

    public void Start()
    {
        staminaCurrent = staminaMax;
        depleteUI.DisplayInitialise();
    }
    //This would be the job of a game manager I think, I purely have it here so that the ui works.
    //TLDR: Remove 'DisplayInitialise' later

    private void FixedUpdate()
    {
        if (staminaCurrent <= staminaMax)
        {
            staminaCurrent =+10;
        }

        if (staminaCurrent >= staminaMax)
        {
            staminaCurrent = staminaMax;
            OnStaminaFullEvent.Invoke();
        }
    }
    //The Idea is that stamina is constantly filling up and then when you sprint, the drain is larger than the
    //refill. When refilled, evoke the StaminaFull Event to tell the SprintTest you can run again.
    

    [FormerlySerializedAs("OnStaminaDepletion")] public UnityEvent StaminaDepletedEvent = new UnityEvent();
    public UnityEvent OnStaminaFullEvent = new UnityEvent();
    public UnityEvent OnStaminaUsageEvent = new UnityEvent();

    //These aren't really needed. I put them here after exploring Interfaces, and these were things I could
    //parse through the interface. I don't think its particularly useful functions in this case, but could
    //be useful later.
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

    public void OnUse(int staminaUsage)
    {
        if(staminaCurrent == staminaMax)
        {
            staminaCurrent = staminaMax;
            staminaCurrent = staminaCurrent - staminaUsage;
            OnStaminaUsageEvent.Invoke();
        }
        else
        {
            staminaCurrent = staminaCurrent - staminaUsage;
            OnStaminaUsageEvent.Invoke();
            if (staminaCurrent <= staminaMin)
            {
                StaminaDepletedEvent.Invoke();
            }
        };
    }
    
}
