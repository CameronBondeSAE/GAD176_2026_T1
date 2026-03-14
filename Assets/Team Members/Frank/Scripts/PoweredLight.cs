using Frank;
using UnityEngine;

public class PoweredLight : MonoBehaviour, ISwitchable
{
    public bool activated = false;
    public Light PoweredLightRef;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Activate()
    {
        PoweredLightRef.enabled = !activated;
        activated = !activated;
    }
}
