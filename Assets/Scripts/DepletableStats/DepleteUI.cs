using Tanks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepleteUI : MonoBehaviour
{
    public Canvas hud;
    public StaminaSys staminaSys;
    public HealthSys healthSys;
    public Slider healthSlider;
    public Slider staminaSlider;
    public TextMeshProUGUI deathMessage;

    private void OnEnable()
    {
        if (staminaSys == null)
        {
            Debug.LogWarning("NULL: staminaSys is not assigned on DepleteUI");
        }

        if (healthSys == null)
        {
            Debug.LogWarning("NULL: healthSys is not assigned on DepleteUI");
            return;
        }

        healthSys.OnDeathEvent.AddListener(DisplayDeathMessage);
        healthSys.OnDamagedEvent.AddListener(DisplayHealthValue);

        if (staminaSys != null)
        {
            staminaSys.OnStaminaUsageEvent.AddListener(DisplayStaminaValue);
        }
    }

    private void OnDisable()
    {
        if (healthSys != null)
        {
            healthSys.OnDeathEvent.RemoveListener(DisplayDeathMessage);
            healthSys.OnDamagedEvent.RemoveListener(DisplayHealthValue);
        }

        if (staminaSys != null)
        {
            staminaSys.OnStaminaUsageEvent.RemoveListener(DisplayStaminaValue);
        }
    }

    public void DisplayInitialise()
    {
        if (healthSlider != null && healthSys != null)
        {
            healthSlider.maxValue = healthSys.MaxValue();
            healthSlider.minValue = healthSys.MinValue();
            healthSlider.value = healthSys.CurrentValue();
        }

        if (staminaSlider != null && staminaSys != null)
        {
            staminaSlider.maxValue = staminaSys.staminaMax;
            staminaSlider.minValue = staminaSys.staminaMin;
            staminaSlider.value = staminaSys.staminaCurrent;
        }

        Debug.Log("UI Bar Display Initialised");
    }

    public void DisplayHealthValue()
    {
        if (healthSlider != null && healthSys != null)
        {
            healthSlider.value = healthSys.CurrentValue();
        }
        else
        {
            Debug.LogWarning("NULL: healthSlider or healthSys is not assigned on DepleteUI");
        }
    }

    public void DisplayStaminaValue()
    {
        if (staminaSlider != null && staminaSys != null)
        {
            staminaSlider.value = staminaSys.staminaCurrent;
        }
        else
        {
            Debug.LogWarning("NULL: staminaSlider or staminaSys is not assigned on DepleteUI");
        }
    }

    public void DisplayDeathMessage()
    {
        if (deathMessage != null)
        {
            deathMessage.enabled = true;
        }
        else
        {
            Debug.LogWarning("NULL: deathMessage TMP is not assigned on DepleteUI");
        }
    }
}