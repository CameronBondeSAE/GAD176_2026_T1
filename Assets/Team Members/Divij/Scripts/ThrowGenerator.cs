using System;
using System.Collections;
using System.Collections.Generic;
using Divij;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Divij
{
    
    public partial class ThrowGenerator : MonoBehaviour, IInteractable
    {
        
        //[SerializeInterface] public IConnected thingConnected;

        public float powerRadius = 3f;
        public bool isOn = true;

        [SerializeInterface] public List<IPowered> connectedList = new List<IPowered>();
        
        

        public void SphereScan()
        {
            
            connectedList.Clear();
            Collider[] hits = Physics.OverlapSphere(transform.position, powerRadius);

            foreach (var hit in hits)
            {
                IPowered powered = hit.GetComponent<IPowered>();

                if (powered != null && !connectedList.Contains(powered))
                {
                    connectedList.Add(powered);
                }
            }
        }

        public void ApplyPower()
        {
            foreach (var powered in connectedList)
            {
                powered?.SetPowered(isOn);
            }
        }


        public void Interact()
        {
            
            isOn = !isOn;
            
            SphereScan();
            ApplyPower();
            
        }
    }
}



/*
 
 generatorOn{
    create the interacted sphere
    add items within this sphere to the connectedList
    run through each item on the list and change their powerStatus to true (This meaning its on)
    
    decrease the generators overall charge (100) by either 5 per second or 1 per item on the list
    When off the generator can be picked up again and hand cranked to charge it up
    }

*/


/* How the generator interacting with objects works
    1. Cast the sphere (Maybe call it some sort of check)
    2. Run a check to first check if the objects interacted with uses the IInteractable interface
    3. If they do then add them to the list, if not do nothing with them
    
    (Use a bool to see if the object is already switched on or not) - Store the objects current state in a variable
        so when the generator stops working it reverts to the previous state instead of switching off. 
        - This is cause it could mess up doors that are already open or lights that are already on. (If charge is independent
            of the amount of objects interacted with this will work)
    4. Once theyre in the list run a coroutine to first switch them on and then incrementally turn down the generators charge
*/

/* How the generator charge up works
 
    The generator will charge up using a coroutine where once you press a button to charge it:
        1. emits a sound for creatures to find you
        2. slows the players movement by some factor
        3. Adds 5 charge per second its held (Use a waitforsecond before the first charge to avoid button spamming to insta charge)
         - will be looped until its max charged. 
        4. Once fully charged it will fail the check to start the charge of the coroutine so it cant be overcharged

*/