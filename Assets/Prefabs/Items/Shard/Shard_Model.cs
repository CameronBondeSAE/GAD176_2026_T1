using Frank;
using UnityEngine;
using UnityEngine.Events;

public class Shard_Model : MonoBehaviour, IPickup
{
	// Variables for totals, to be read by IPowered things
	[SerializeField, Tooltip("Reference to the total power the shard has")]
	private float maxEnergy = 100f;
	[SerializeField, Tooltip("Reference to the current energy")]
	private float currentEnergy;

	/// <summary>
	/// Reference to the current energy of this shard
	/// </summary>
	public float CurrentEnergy => currentEnergy;

	/// <summary>
	/// Trigger when the shard energy reaches 0
	/// </summary>
	public UnityEvent onShardDestroy;
		
		
	private void Start()
	{
		currentEnergy = maxEnergy;
	}
	
	public bool UseEnergy(int amount)
	{
		currentEnergy -= amount;
		if (currentEnergy <= 0)
		{
			onShardDestroy?.Invoke();
			return false;
		}

		return true;
	}
	

}
