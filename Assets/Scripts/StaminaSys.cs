using UnityEngine;
using UnityEngine.UIElements;

public class StaminaSys : MonoBehaviour, IDepletableBars
{
    public int staminaMax;
    public int staminaCurrent;
    public int staminaMin;
    public Slider staminaDisplay;
    

    public void UiDisplayUpdate()
    {
        
    }

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
        return staminaCurrent;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
