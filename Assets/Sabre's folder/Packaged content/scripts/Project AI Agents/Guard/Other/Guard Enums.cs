using UnityEngine;
namespace Sabre.AI
{
public enum GuardScenarioEnums
{
	ObjectSecured = 0,
	NoThieves = 1,
	Nothingheld = 2,
	BoxHeld = 3,
	FightThief = 4,
	BoxInOpen = 5,
	WeaponHeld = 6,
	NearWeapon = 7,
	ThiefDefeated = 8,
	ThiefInRange = 9,
	Dead = 10
}

public enum ThiefAI
{
	FoundBox = 0,
	NothingHeld = 1,
	BoxHeld = 2,
	WeaponHeld = 3,
	Fightingguard = 4,
	EscapeGuard = 5,
	BoxHeldByGuard = 6,
	FoundWeapon = 7
}
}