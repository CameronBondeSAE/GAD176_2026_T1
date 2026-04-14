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

    public void ReceivePower(bool powered)
    {
        if (connectedCable != null)
        {
            connectedCable.SetPowered(powered);
        }
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
