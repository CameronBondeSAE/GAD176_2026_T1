using UnityEngine;

namespace DefaultNamespace
{
	public class Interactable_Base : MonoBehaviour
	{
		public int thing;
		public string stuff;
		
		public virtual void Interact()
		{
			Debug.Log("Interact base");
		}
	}
}