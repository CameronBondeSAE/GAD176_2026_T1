using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthSys : NetworkBehaviour, IDepletableBars
{
    public int healthMax = 100;
    public int healthMin = 0;
    public DepleteUI depleteUI;

    private NetworkVariable<int> healthCurrent = new NetworkVariable<int>(100, NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server);

    [FormerlySerializedAs("Damaged")] public UnityEvent OnDamagedEvent = new UnityEvent();

    [FormerlySerializedAs("OnHealthDepletion")]
    public UnityEvent OnDeathEvent = new UnityEvent();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            healthCurrent.Value = healthMax;
        }

        healthCurrent.OnValueChanged += OnHealthChanged;
        OnDeathEvent.AddListener(HackDeath);


        if (depleteUI != null)
        {
            depleteUI.DisplayInitialise();
        }
    }

    public override void OnNetworkDespawn()
    {
        healthCurrent.OnValueChanged -= OnHealthChanged;
        OnDeathEvent.RemoveListener(HackDeath);
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
        return healthCurrent.Value;
    }

    /*public void OnDmg(int healthNegative)
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
        }*/

    public void Damage(int amount)
    {
        if (amount <= 0)
        {
            return;
        }

        if (IsServer)
        {
            healthCurrent.Value -= amount;
        }
        else
        {
            DamageRpc(amount);
        }
    }

    [Rpc(SendTo.Server)]
    private void DamageRpc(int amount)
    {
        healthCurrent.Value -= amount;
    }

    private void OnHealthChanged(int previousHealth, int newHealth)
    {
        OnDamagedEvent.Invoke();

        if (newHealth <= healthMin)
        {
            OnDeathEvent.Invoke();
            Debug.Log("You Dead!");
        }
    }

    public void HackDeath()
    {
        if (!IsServer)
        {
            return;
        }

        NetworkObject.Despawn(true);
    }
}