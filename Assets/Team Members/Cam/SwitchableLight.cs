using UnityEngine;

namespace CameronBonde
{
	public class SwitchableLight : MonoBehaviour, IInteractable
	{
		public Light light;


		// This is the interface entry point
		public void Interact()
		{
			Toggle();
		}


		public void Toggle()
		{
			Debug.Log("SwitchableLight: Toggle");
			light.enabled = !light.enabled;
		}
	}
}