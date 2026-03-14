using Frank;
using UnityEngine;

public class SwitchableLight : MonoBehaviour, ISwitchable
{
    public bool activated = false;
    public Light SwitchableLightRef;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Activate(bool poweredState)
    {
        if (poweredState == true)
        {
            SwitchableLightRef.enabled = !activated;
            activated = !activated;
        }
        else
        {
            SwitchableLightRef.enabled = false;
        }
        //SwitchableLightRef.enabled = !activated;
        //activated = !activated;
    }
}
