using System.Collections.Generic;
using CameronBonde;
using UnityEngine;


// NOTE: The 'partial' keyword here is just for the Interfaces plugin to work
public partial class Button : MonoBehaviour, IInteractable
{
	// Add this to any interface variables to enable drag-drop in Unity
	[SerializeInterface]
	public List<IInteractable> thingsToInteractWith;
	
    public void Interact()
    {
	    // eg a Designer could hook this up to a door/light/OTHER switches etc
	    foreach (IInteractable interactable in thingsToInteractWith)
	    {
		    interactable.Interact();
	    }
    }
}
