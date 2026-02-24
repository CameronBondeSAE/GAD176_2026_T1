using CameronBonde;
using UnityEngine;


namespace CameronBonde
{
	// NOTE: The 'partial' keyword here is just for the Interfaces plugin to work
	public class Button : MonoBehaviour, IInteractable
	{
		// Add this to any interface variables to enable drag-drop in Unity
		[SerializeInterface]
		public IInteractable thingToInteractWith;

		[SerializeInterface]
		public IInteractable[] thingsToInteractWith;

		public void Interact()
		{
			// eg a Designer could hook this up to a door/light/OTHER switches etc
			thingToInteractWith.Interact();
		}
	}
}
