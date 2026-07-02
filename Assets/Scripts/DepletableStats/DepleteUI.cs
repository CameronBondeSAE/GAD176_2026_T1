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
        staminaSys.OnStaminaUsageEvent.AddListener(DisplayStaminaValue);
    }

    private void OnDisable()
    {
        healthSys.OnDeathEvent.RemoveListener(DisplayDeathMessage);
        healthSys.OnDamagedEvent.RemoveListener(DisplayHealthValue);
        staminaSys.OnStaminaUsageEvent.RemoveListener(DisplayStaminaValue);
    }

    public void DisplayInitialise()
    {
        if(healthSlider && staminaSlider != null)
        {
            healthSlider.maxValue = healthSys.healthMax;
            healthSlider.minValue = healthSys.healthMin;
            staminaSlider.maxValue = staminaSys.staminaMax;
            staminaSlider.minValue = staminaSys.staminaMin;
            healthSlider.value = healthSlider.maxValue;
            staminaSlider.value = staminaSlider.maxValue;
            Debug.Log("UI Bar Display Initialised");
        }
        else
        {
            Debug.LogWarning("NULL: healthSlider OR staminaSlider is not assigned on DepleteUI");
        }
    }

    public void DisplayHealthValue()
    {
        if (healthSys != null)
        {
            healthSlider.value = healthSys.healthCurrent;
        }
        else
        {
            Debug.LogWarning("NULL: deathMessage TMP is not assigned on DepleteUI");
        }

    }

    public void DisplayStaminaValue()
    {
        if (staminaSlider != null)
        {
            staminaSlider.value = staminaSys.staminaCurrent;
        }
        else
        {
            Debug.LogWarning("NULL: stamina slider is not assigned on DepleteUI");
        }
    }
    
    public void DisplayDeathMessage()
    {
        if(deathMessage != null)
        {deathMessage.enabled = true;}
        else{Debug.LogWarning("NULL: deathMessage TMP is not assigned on DepleteUI");}
    }

}
