using Divij;
using UnityEngine;

public class AutomaticDoor : MonoBehaviour, IPowered
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public bool isPowered; // the variable used to determine whether or not to run the     function that controls the door's observable function - to open or to close. 
    public Transform playerDetectorRef;
    
    private void Update()
    {
        if(isPowered)
        {
            ActionIfPowered();
            Debug.Log("ActionIfPowered function is working");
        }; // for objects that are continuously checking for collisions or input, I think an update function is more appropriate than using the IInteractable interface.
    }
	
    public void SetPowered(bool powered) // the generator supplies the value "true" from the IsOn variable in the generator script when the generator is on and "false" when the generator is off
    {
        isPowered = powered;
        Debug.Log(isPowered);
        
    }
	
    public void ActionIfPowered()
    {
        Physics.Raycast(playerDetectorRef.position, Vector3.up, out RaycastHit hit, 2f);
        Debug.Log(hit);
    }
}
