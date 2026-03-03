using System.Collections.Generic;
using Frank;
using UnityEngine;

public class GeneralSwitch: MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public List<IInteractable> interactableList = new List<IInteractable>();

    public void Interact()
    {
        foreach (IInteractable currentInteractable in interactableList)
        {
            currentInteractable.Interact();
        }
    }
}



