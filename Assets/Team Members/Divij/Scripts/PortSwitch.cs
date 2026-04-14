using Divij;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour, IInteractable
{
    /*
    public PowerPort port;
    public void Interact()
    {
        Debug.Log("Switch Activated");
        port.ToggleSwitch();
    }
    
    */
    public void Interact()
    {
        throw new System.NotImplementedException();
    }
}

/*
 *       Switch just needs to use the ToggleSwitch() in powerport on its interact.
 *       Also saves the port attached to it as a variable thus it interacts with the correct port.
 */
