using Frank;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable //this needs to be IConnected
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Transform doorHeight;
    public bool open = false;

    public void Interact()
    {
        if (open == false)
        {
            open = true;
            OpenDoor();
            
        }

        else if (open == true)
        {
            open = false;
            CloseDoor();
        }
    }



    void OpenDoor()
    {
            doorHeight.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
    }
    
     void CloseDoor()
     {

         doorHeight.position = new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z);
     }
}
