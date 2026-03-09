using Divij;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour, IPowered
{
    public bool isPowered;
    public Transform playerDetectorTransform;
    

    public void SetPowered(bool powered) // the generator supplies the value "true" from the IsOn variable in the generator script when the generator is on and "false" when the generator is off
    {
        isPowered = powered;
        
    }

    private void Start()
    {
        
    }
    
    private void Update()
    {

        if (isPowered)
        {
            ActionIfPowered();
        }
        // for objects that are continuously checking for collisions or input, I think an update function is more appropriate than using the IInteractable interface.
    }


    public void ActionIfPowered()
    {
        Ray rayDraw = new Ray(playerDetectorTransform.position, Vector3.up);
        RaycastHit hit;

        Debug.DrawRay(rayDraw.origin, rayDraw.direction * 5f, Color.red);

        Physics.Raycast(playerDetectorTransform.position, Vector3.up, out hit, 2f);
        if (hit.collider != null && hit.collider.tag == "Player")
        {

            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;

        }
        else
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        
    }
}