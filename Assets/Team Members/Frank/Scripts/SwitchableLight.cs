using Frank;
using UnityEngine;

public class SwitchableLight : MonoBehaviour, ISwitchable
{
    public bool activated = false;
    public Light SwitchableLightRef;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Activate()
    {
        SwitchableLightRef.enabled = !activated;
        activated = !activated;
    }
}
