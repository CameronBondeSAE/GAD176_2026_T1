using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class StaminaSys : NetworkBehaviour, IDepletableBars
{
    public int staminaMax = 100;
    public int staminaMin = 0;
    public int staminaUsage;
    public DepleteUI depleteUI;

    private NetworkVariable<int> staminaCurrent = new NetworkVariable<int>(100, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    [FormerlySerializedAs("OnStaminaDepletion")]
    public UnityEvent StaminaDepletedEvent = new UnityEvent();

    public UnityEvent OnStaminaFullEvent = new UnityEvent();
    public UnityEvent OnStaminaUsageEvent = new UnityEvent();

    /*public void Start()
    {
        staminaCurrent = staminaMax;
        if (depleteUI != null) depleteUI.DisplayInitialise();
    }*/
    //This would be the job of a game manager I think, I purely have it here so that the ui works.
    //TLDR: Remove 'DisplayInitialise' later

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            staminaCurrent.Value = staminaMax;
        }

        staminaCurrent.OnValueChanged += OnStaminaChanged;

        if (depleteUI != null)
        {
            depleteUI.DisplayInitialise();
        }
    }

    public override void OnNetworkDespawn()
    {
        staminaCurrent.OnValueChanged -= OnStaminaChanged;
    }

    private void FixedUpdate()
    {
        if (!IsServer)
        {
            return;
        }

        if (staminaCurrent.Value < staminaMax)
        {
            staminaCurrent.Value += 1;

            if (staminaCurrent.Value >= staminaMax)
            {
                staminaCurrent.Value = staminaMax;
                OnStaminaFullEvent.Invoke();
            }
        }
    }
    //The Idea is that stamina is constantly filling up and then when you sprint, the drain is larger than the
    //refill. When refilled, evoke the StaminaFull Event to tell the SprintTest you can run again.

    //These aren't really needed. I put them here after exploring Interfaces, and these were things I could
    //parse through the interface. I don't think its particularly useful functions in this case, but could
    //be useful later.
    public int MaxValue()
    {
        return staminaMax;
    }

    public int MinValue()
    {
        return staminaMin;
    }

    public int CurrentValue()
    {
        return staminaCurrent.Value;
    }

    public void UseStamina(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (IsServer)
        {
            staminaCurrent.Value -= amount;
        }
        else
        {
            UseStaminaRpc(amount);
        }
    }
    
    [Rpc(SendTo.Server)]
    private void UseStaminaRpc(int amount)
    {
        staminaCurrent.Value -= amount;
    }

    private void OnStaminaChanged(int previousStamina, int newStamina)
    {
        OnStaminaUsageEvent.Invoke();

        if (newStamina <= staminaMin)
        {
            staminaCurrent.Value = staminaMin;

            StaminaDepletedEvent.Invoke();
        }
    }
}
