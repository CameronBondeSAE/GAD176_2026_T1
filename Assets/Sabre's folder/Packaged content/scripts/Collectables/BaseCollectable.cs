using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Sabre.AI
{
public enum CollectableType {None, Box, Weapon}
public class BaseCollectable : MonoBehaviour
{
    public CollectableType ObjectType;
    public bool currentlyHeld;
    public Inventory Owner;
    public bool overridePickup;
    [SerializeField] private float Cooldowntime = 3f;
    [SerializeField] private Collider collider;

    void Awake()
    {
        if(collider == null)
        {
            collider = this.gameObject.GetComponent<Collider>();
        }
    }

    private void Start()
    {
        Drop();
    }

    public void PickUp(Inventory owner)
    {
        Owner = owner;
        currentlyHeld = true;
        collider.enabled = false;
    }

    public void Drop()
    {
        Owner = null;
        collider.enabled = true;

        if(ObjectType == CollectableType.Weapon)
        {
            PickUpOverride();
        }
        else
        {
            currentlyHeld = false;
        }
    }

    private IEnumerator PickUpOverride()
    {
        overridePickup = true;
        yield return new WaitForSeconds(Cooldowntime);
        overridePickup = false;
    }
}
}