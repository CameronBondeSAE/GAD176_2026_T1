using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
namespace Sabre.AI
{
public class GuardSense : MonoBehaviour, ISense
{
    //public bool ObjectSecured;
	//public bool NoThieves;
    
    public GameObject CurrentTarget;
	public bool Nothingheld;
	public bool BoxHeld;
    public bool WeaponHeld;
    public bool NearWeapon;


	public bool FightThief;
	public bool ThiefDefeated;
    public bool Dead;

    public BoxOBJReference boxStation;
    [SerializeField] private Neighbors localNeighbors;
    public Inventory inventory;
    public GameObject pathfind;
    public Health health;


    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {

        //ObjectSecured = boxStation.BoxSafe;
        boxStation.CheckBoxSafety();
        boxStation.CheckBoxOutInOpen();
        
        aWorldState.Set(GuardScenarioEnums.ObjectSecured, boxStation.BoxSafe);
        aWorldState.Set(GuardScenarioEnums.NoThieves, CheckThiefs());

        CheckObject();
        aWorldState.Set(GuardScenarioEnums.NearWeapon, NearWeapon);

        aWorldState.Set(GuardScenarioEnums.Nothingheld, Nothingheld);
        aWorldState.Set(GuardScenarioEnums.BoxHeld, BoxHeld);
        aWorldState.Set(GuardScenarioEnums.WeaponHeld, WeaponHeld);

        aWorldState.Set(GuardScenarioEnums.FightThief, FightThief);
        aWorldState.Set(GuardScenarioEnums.ThiefDefeated, ThiefDefeated);
        
        aWorldState.Set(GuardScenarioEnums.BoxInOpen, boxStation.BoxInOpen);
        aWorldState.Set(GuardScenarioEnums.Dead, health.DeadBool);
        
    }

    private void CheckObject()
    {
        Nothingheld = false;
        BoxHeld = false;
        WeaponHeld = false;
        NearWeapon = false;

        switch(inventory.heldType)
        {
            case CollectableType.None:
                {
                    Nothingheld = true;
                    break;
                }
            case CollectableType.Box:
                {
                    Debug.Log("box is currently being held");
                    BoxHeld = true;
                    break;
                }
            case CollectableType.Weapon:
                {
                    WeaponHeld = true;
                    break;
                }
        }

        if(inventory.nearCollectables.Count > 0)
        {

            foreach(BaseCollectable collectable in inventory.nearCollectables)
            {
                if(collectable != null && collectable.currentlyHeld == false )
                {
                    if(collectable.ObjectType == CollectableType.Weapon && collectable.overridePickup == false)
                    {
                        NearWeapon = true;
                    }
                }
            }
        }
    }

    private bool CheckThiefs()
    {
        CurrentTarget = null;
        foreach(Transform player in localNeighbors.neighborsList)
        {
            Health playHealth = player.gameObject.GetComponent<Health>();

            if(playHealth != null)
            {
                if(playHealth.type == AIType.Thief && playHealth.DeadBool == false)
                {
                    CurrentTarget = player.gameObject;
                    return false;
                }
            }
        }
        return true;
    }


}

}
