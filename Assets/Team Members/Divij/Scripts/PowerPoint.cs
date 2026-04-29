using Divij;
using UnityEngine;

public class PowerPoint : MonoBehaviour, IPowered, IInteractable
{
    public PowerCable connectedCable;

    private bool isPowered;
    
    
    public void Interact()
        {
            Debug.Log("PowerPoint selected");
            
        }
    public void SetPowered(bool powered)
    {
        isPowered = powered;

        ReceivePower(powered);
    }

    public bool GetPowered()
    {
	    return isPowered;
    }

    public void ReceivePower(bool powered)
    {
        if (connectedCable != null)
        {
            connectedCable.SetPowered(powered);
        }
    }

    // TODO: Swap to accept an IPowered. Called from sometihng like the power cable
    public void ConnectCable(PowerCable cable)
    {
        connectedCable = cable;
    }

    public void DisconnectCable()
    {
        connectedCable = null;
    }

    
}
