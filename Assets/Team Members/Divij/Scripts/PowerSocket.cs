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

/*
 *    Power Socket will have a list of connected devices which will be set in the scene,
 *      now we can set a single socket to an array of lights or just multiple different powered devices
 */
