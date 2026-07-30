using Frank;
using UnityEngine;

public class CamsPickupTest : MonoBehaviour, IPickup
{
	public void Pickup(Transform parent)
	{
		Debug.Log("Pickup");
	}
}
