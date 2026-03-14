using UnityEngine;
using Frank;
using Divij;

public class PoweredLight : MonoBehaviour, IPowered
{
    public Light poweredLightRef;
    
    public void SetPowered(bool powered)
    {
        if (powered)
        {
            poweredLightRef.enabled = !poweredLightRef.enabled;
        }

        else
        {
            poweredLightRef.enabled = false;
        }
    }
}
