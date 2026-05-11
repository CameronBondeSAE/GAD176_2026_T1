using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CameronBonde;
using Sabre.AI;

public enum CollectableType {None, Box, Weapon}
public class BaseCollectable : MonoBehaviour, IInteractable
{
    public CollectableType ObjectType;
    [SerializeField] private Vector3 HeldPosition = new Vector3(0,0, 1f);
    [SerializeField] private float dropHeight = -3.3f;
    public bool currentlyHeld;
    public CharacterBase Owner; // suppose to be inventory, however doesn't include non-inventory characters
    public bool overridePickup;
    [SerializeField] private float Cooldowntime = 10f;
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

    public virtual void Interact()
    {
        if(currentlyHeld == true)
        {
            return;
        }

        CharacterBase tempchar = null;
        float chardist = 100f;

        int layerMask = LayerMask.GetMask("Player");
        Collider[] hitColliders = new Collider[30]; 
        Physics.OverlapSphereNonAlloc(transform.position, 100f, hitColliders);
        foreach(Collider coll in hitColliders)
        {
            CharacterBase CollChar = coll.gameObject.GetComponent<CharacterBase>();
            Debug.Log("checking if: " + coll.gameObject + " is a player: " + CollChar);

            if(collider != null && CollChar != null)
            {
                float checkdist = Vector3.Distance(this.gameObject.transform.position, coll.transform.position);
                if(checkdist < chardist)
                {
                    tempchar = CollChar;
                    chardist = checkdist;
                }
            }
        }

        if(tempchar == null)
        {
            Debug.Log("no player character found to be held");
            return;
        }

        Owner = tempchar;
        
        currentlyHeld = true;
        collider.enabled = false;

        transform.position = transform.position + HeldPosition;
        transform.SetParent(Owner.gameObject.transform);
    }


    public void Drop()
    {
        Owner = null;
        collider.enabled = true;

        if(ObjectType == CollectableType.Weapon)
        {
            PickUpOverride();
        }
        
        currentlyHeld = false;

        transform.position = new Vector3(transform.position.x, dropHeight, transform.position.z);
        transform.SetParent(null);
        
    }

    private IEnumerator PickUpOverride()
    {
        overridePickup = true;
        yield return new WaitForSeconds(Cooldowntime);
        overridePickup = false;
    }
}
