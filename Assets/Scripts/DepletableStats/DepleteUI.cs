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
    
    

    public void DisplayInitialise()
    {
        healthSlider.maxValue = healthSys.healthMax;
        healthSlider.minValue = healthSys.healthMin;
        staminaSlider.maxValue = staminaSys.staminaMax;
        staminaSlider.minValue = staminaSys.staminaMin;
        healthSlider.value = healthSlider.maxValue;
        staminaSlider.value = staminaSlider.maxValue;
        Debug.Log("Display Initialised");
    }

    public void DisplayHealthValue()
    {
        healthSlider.value = healthSys.healthCurrent;
    }

    public void DisplayStaminaValue()
    {
        staminaSlider.value = staminaSys.staminaCurrent;
    }

    public void DisplayDeathMessage()
    {
        deathMessage.enabled = true;
    }

}
