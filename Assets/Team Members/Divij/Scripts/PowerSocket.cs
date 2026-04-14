using System.Collections.Generic;
using Divij;
using UnityEngine;

public class PowerSocket : MonoBehaviour, IPowered, IInteractable
{

    public PowerCable connectedCable;
    public List<MonoBehaviour> connectedDevices = new();

    private bool isPowered;
    
    

    public void SetPowered(bool powered)
    {
        isPowered = powered;

        foreach (var device in connectedDevices)
        {
            if (device is IPowered poweredDevice)
            {
                poweredDevice.SetPowered(powered);
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Socket Selected");
        
    }

    public void ConnectCable(PowerCable cable)
    {
        connectedCable = cable;
    }

    public void DisconnectCable()
    {
        connectedCable = null;
    }
    
    
}
