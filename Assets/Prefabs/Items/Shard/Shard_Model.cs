using Frank;
using UnityEngine;

public class Shard_Model : MonoBehaviour, IPickup
{
	// Variables for totals, to be read by IPowered things
	
	public bool UseEnergy(int amount)
	{
		// Check for totals
		// Take off amount from your total
		// Destroy if zero

		return true; // TODO: This should return false if there's not enough energy to supply the thing
	}
}
