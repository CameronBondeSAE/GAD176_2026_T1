using System.Collections;
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
    public RaycastHit hitInfo;
    public float radius = 1f;

    public void SetPowered(bool powered) // the generator supplies the value "true" from the IsOn variable in the generator script when the generator is on and "false" when the generator is off
    {
        isPowered = powered;
        
    }

    public bool GetPowered()
    {
	    return isPowered;
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

        Physics.SphereCast(playerDetectorTransform.position, radius, Vector3.down, out hitInfo, 2f);
        if ((hitInfo.collider != null || secondPlayerDetector.GetComponent<PlayerDetection>().hitInfo.collider != null) && (hitInfo.collider.tag == "Player" || secondPlayerDetector.GetComponent<PlayerDetection>().hitInfo.collider.tag == "Player"))
        {

            gameObjectRef.GetComponent<ISwitchable>().Activate(true);
            
        }
        else
        {
            gameObjectRef.GetComponent<ISwitchable>().Activate(false);
        }
        
    }

    
}