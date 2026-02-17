using System;
using CameronBonde;
using DefaultNamespace;
using UnityEngine;


// NOTE: The 'partial' keyword here is just for the Interfaces plugin to work
public partial class Button : Interactable_Base
{
	// Add this to any interface variables to enable drag-drop in Unity
	[SerializeInterface]
	public IInteractable thingToInteractWith;

	private void Start()
	{
		throw new NotImplementedException();
	}
}
