using Divij;
using Frank;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDetection : MonoBehaviour, IPowered
{
    public bool isPowered;
    public Transform playerDetectorTransform;
    public GameObject gameObjectRef;
    public GameObject secondPlayerDetector;
    public float radius = 1f;

    public void SetPowered(bool powered) // the generator supplies the value "true" from the IsOn variable in the generator script when the generator is on and "false" when the generator is off
    {
        isPowered = powered;
        
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
        Ray rayDraw = new Ray(playerDetectorTransform.position, Vector3.down);
        RaycastHit hit;

        Debug.DrawRay(rayDraw.origin, rayDraw.direction * 2f, Color.red);

        Physics.SphereCast(playerDetectorTransform.position, radius, Vector3.down, out hit, 2f);
        if (hit.collider != null && hit.collider.tag == "Player")
        {

            gameObjectRef.GetComponent<IInteractableWithState>().Interact(true);
            secondPlayerDetector.SetActive(false);

        }
        else
        {
            gameObjectRef.GetComponent<IInteractableWithState>().Interact(false);
            secondPlayerDetector.SetActive(true);
        }
        
    }
}