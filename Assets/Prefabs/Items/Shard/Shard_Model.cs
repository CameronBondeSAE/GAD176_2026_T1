using Divij;
using Frank;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

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
	public UnityEvent<Shard_Model> onShardDestroy;

	/// <summary>
	/// Invoked when a shard is picked up
	/// </summary>
	public UnityEvent<Shard_Model> onShardPickedUp;
		
		
	private void Start()
	{
		currentEnergy = maxEnergy;
	}
	
	private void OnTriggerEnter(Collider other)
	{

		IPowered target = other.GetComponentInChildren<IPowered>();
		if(target != null)
			target.ReceivePotentialEnergy(this);
	}
	
	private void OnTriggerExit(Collider other)
	{
		IPowered target = other.GetComponentInChildren<IPowered>();
		if(target != null)
			target.PotentialEnergyRemoved(this);
	}

	public void PickupShard()
	{
		onShardPickedUp?.Invoke(this);
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
			onShardDestroy?.Invoke(this);
			return false;
		}

		return true;
	}
}
