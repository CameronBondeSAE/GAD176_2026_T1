using Divij;
using Unity.VisualScripting;
using UnityEngine;
using IInteractable = CameronBonde.IInteractable;

public class PowerPort : MonoBehaviour, IInteractable
{
    public bool isOutput;
    public bool switchOn = true;

    public MonoBehaviour connectedDevice;
    public void Interact()
    {
        Debug.Log("Port Chosen");
        ConnectionManager.Instance.SelectPort(this);
        
    }

    public void ToggleSwitch()
    {
        switchOn = !switchOn;
        Debug.Log(switchOn ? "Switch On" : "Switch Off");

        ConnectionManager.Instance.UpdatePower();
    }
}


/* 
        Needs a boolean to check whether the specific port interacted with is an input or an output port 
        
        Also should have the ToggleSwitch Function as it will be used to Update power status, whether its startd or stopped.
        Also will keep track of whether the switch is on or off by default

*/