using UnityEngine;

// PROBLEM: AI's have no idea if another object is a character or not
// eg if an attacks other characters, it has no idea
// DON'T hardcode this. eg "FindAllObjectsOfType<Player>()", this is lame, there's better ways without having specific code for every type of character
// There's nothing wrong with hardcoding btw. Sometimes you really do want specific code, eg your own species, or just the player etc

// SOLUTIONS:
// Use a base class
// Use an interface
// Use a component

public class CharacterBase : MonoBehaviour
{
	public int startingHealth;
	public int maxHealth;
	public int currentHealth;

	public int startingEnergy;
	public int maxEnergy;
	public int currentEnergy;
	public int lowEnergyThreshold;

	public bool isAlive;

	public void UseEnergy(int amount)
	{
		currentEnergy -= amount;
		if (currentEnergy <= lowEnergyThreshold)
		{
			LowEnergy();
		}
	}

	public virtual void LowEnergy()
	{
		
	}

	public void Damage(int amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;

			Die();
		}
	}

	public virtual void Die()
	{
		isAlive = false;
		Debug.Log("DIE! : "+gameObject.name);
	}

	public void Reset()
	{
		currentHealth = startingHealth;
		currentEnergy = startingEnergy;
		isAlive = true;
	}
}
