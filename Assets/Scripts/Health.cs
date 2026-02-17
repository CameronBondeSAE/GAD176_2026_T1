using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class Health : MonoBehaviour
	{
		public int amount;
		public event Action OnDeath;
		public event Action OnDamage;
		
		public void Damage(int _amount)
		{
			amount -= _amount;

			OnDamage?.Invoke();
		}
	}
}