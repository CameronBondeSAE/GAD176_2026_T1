using System.Collections.Generic;
using CameronBonde;
using SerializeInterface.Samples;
using UnityEngine;


namespace CameronBonde
{
	// NOTE: The 'partial' keyword here is just for the Interfaces plugin to work
	public partial class Button : MonoBehaviour
	{
		// Add this to any interface variables to enable drag-drop in Unity
		[SerializeInterface]
		private IInteractable thingToInteractWith;

		[SerializeInterface]
		private IInteractable[] thingsToInteractWith;

		[SerializeInterface]
		private IGeneric<int> _intGeneric;
        
		[SerializeInterface]
		private IGeneric<string> _stringGeneric;

		[SerializeInterface]
		private List<IGeneric<int>> _listGeneric;

		[SerializeInterface] 
		private IGeneric<IGeneric<int>> _nestedGeneric;


		
		public void Interact()
		{
			// eg a Designer could hook this up to a door/light/OTHER switches etc
			thingToInteractWith.Interact();
		}
	}
}
