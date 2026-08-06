using Divij;
using Frank;
using UnityEngine;
using UnityEngine.Events;

public class Shard_Model : MonoBehaviour, IPickup
{
	// Variables for totals, to be read by IPowered things
	[SerializeField, Tooltip("Reference to the total power the shard has")]
	private int maxEnergy = 100;
	[SerializeField, Tooltip("Reference to the current energy")]
	private int currentEnergy;

	/// <summary>
	/// Reference to the current energy of this shard
	/// </summary>
	public int CurrentEnergy => currentEnergy;

	/// <summary>
	/// Trigger when the shard energy reaches 0
	/// </summary>
	public UnityEvent onShardDestroy;
		
		
	private void Start()
	{
		currentEnergy = maxEnergy;
	}
	
	private void OnTriggerEnter(Collider other)
	{
		
		// Why are we passing this through to the target rather than just the value of the power used?
		// Should we be using a coroutine instead to draw the power so that we can update values overtime?
		// Is this possibly just to check that IPowered has entered and than checks if has enough power to add to IPowered?
		if (other is IPowered target)
		{
			target.ReceivePotentialEnergy(this);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other is IPowered target)
		{
			target.PotentialEnergyRemoved(this);
		}
	}
	
	public bool UseEnergy(int amount)
	{
		// If the current engergy isn't enough 
		// than return false
		if (amount > currentEnergy)
			return false;

		// Remove the energy
		currentEnergy -= amount;
		if (currentEnergy <= 0)
		{
			// Invoke the shard destroyed
			onShardDestroy?.Invoke();
			return true;
		}

		return false;
	}
	

}
