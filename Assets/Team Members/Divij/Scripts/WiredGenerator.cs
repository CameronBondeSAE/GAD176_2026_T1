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
 *  The wired generator will now just stores a list of PowerPoints 
 *
 *     Will use a for each loop to run through the list of powerpoints and supply power to them.
 *          Is currently in the Interact function so the generator will only update power status when switched on
 *          and off but can be put in a coroutine or something else so that it constantly updates the power status to
 *          whatever its meant to be. Not doing it now because laptop is slow. 
 */








/* 
    Want the generator to have 4 ports and a switch for each port.
       - Should be stored in a list where the ports are gameObjects that can be selected and then linked to other ports
       
    Maybe make a seperate script for the ports since there are 'input' and 'output' - Ones that supply and others that need electricity
        - Can just use a bool to set it in the inspector whether a port is a power supplying port or not. 
            - Can then also do a check where there needs to be one that is and one that isnt a power supply so that you dont connect a 
            power port to another power port or an input to another input
            
    Also need to save the powered item as a gameObject so that we can use its transform to draw the wires using Franks script 
    
*/