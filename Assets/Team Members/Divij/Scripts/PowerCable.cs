using Divij;
using UnityEngine;

public class PowerCable : MonoBehaviour, IPowered
{
    public PowerPoint connectedPoint;
    public PowerSocket connectedSocket;

    private bool isPowered;


    public void SetPowered(bool powered)
    {
        isPowered = powered;

        if (connectedSocket != null)
        {
            connectedSocket.SetPowered(powered);
        }
    }

    public bool GetPowered()
    {
	    return isPowered;
    }

    public void ConnectStart(PowerPoint p)
    {
        connectedPoint = p;
        connectedPoint.ConnectCable(this);
    }

    public void ConnectEnd(PowerSocket s)
    {
        connectedSocket = s;
        connectedSocket.ConnectCable(this);
    }

    public void Disconnect()
    {
        connectedPoint?.DisconnectCable();
        connectedPoint?.DisconnectCable();
        
        
        connectedPoint = null;
        connectedSocket = null;
    }
}
