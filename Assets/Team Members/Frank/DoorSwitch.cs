using System.Collections.Generic;
using Frank;
using UnityEngine;

public class DoorSwitch : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public List<Door> doorList = new List<Door>();

    public void Interact()
    {
        foreach (Door currentDoor in doorList)
        {
            currentDoor.Interact();
        }
    }
}



