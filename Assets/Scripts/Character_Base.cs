using UnityEngine;

namespace DefaultNamespace
{
	public class Character_Base : MonoBehaviour
	{
		public int health;

		public virtual void Damage(int amount)
		{
			health -= amount;
		}
	}
}