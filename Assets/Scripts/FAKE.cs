using DefaultNamespace;
using UnityEngine;

public class FAKE : Character_Base
{
	public override void Damage(int amount)
	{
		base.Damage(amount);
		
		Debug.Log(name + " damaged " + amount);
	}
}
