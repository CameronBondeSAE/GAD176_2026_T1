using Divij;using UnityEngine;


namespace Divij
{
	public interface IPowered
	{
		void SetPowered(bool powered);
		bool GetPowered();

		/// <summary>
		///	Optional
		/// Energy provider will call this every now and then. Return how much energy you actually used.
		/// currentEnergy is read directly from the shard reference
		/// </summary>
		/// <param name="shard = THIS, so the energy user can ask it how much energy it has"></param>
		/// <returns>used energy (if no power used yet, return zero)</returns>
		void ReceivePotentialEnergy(Shard_Model shard)
		{
			
		}

		/// <summary>
		/// When a shard moves away from an IPowered, it needs to know
		/// </summary>
		/// <param name="shard is so you can remove from your list of power providers"></param>
		void PotentialEnergyRemoved(Shard_Model shard)
		{
			
		}
	}
}
