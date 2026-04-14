using CameronBonde;
using UnityEngine;
using UnityEngine.Rendering;

public class WiredGenerator : MonoBehaviour, IInteractable
{
    public bool isOn = false;
    
    public PowerPoint[] powerPoints;


    public void Interact()
    {
        isOn = !isOn;
        Debug.Log(isOn ? "Generator on" : "Generator off");

        foreach (var point in powerPoints)
        {
            point.ReceivePower(isOn);
        }
    }
}




/* 
    Want the generator to have 4 ports and a switch for each port.
       - Should be stored in a list where the ports are gameObjects that can be selected and then linked to other ports
       
    Maybe make a seperate script for the ports since there are 'input' and 'output' - Ones that supply and others that need electricity
        - Can just use a bool to set it in the inspector whether a port is a power supplying port or not. 
            - Can then also do a check where there needs to be one that is and one that isnt a power supply so that you dont connect a 
            power port to another power port or an input to another input
            
    Also need to save the powered item as a gameObject so that we can use its transform to draw the wires using Franks script 
    
*/