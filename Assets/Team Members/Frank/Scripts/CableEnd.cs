using System;
using Frank;
using UnityEngine;

// TODO: Implement IPowered
public class CableEnd : MonoBehaviour, IInteractable, IHoldable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool isHeld = false;
    public bool isConnected = false;
    [SerializeField] private Vector3 plugOffset = new Vector3(0, 0, 1);
    public GameObject followCameraRef;
    public Transform powerPointTransformRef;
    public Transform playerHandsTransformRef;

    public void Interact()
    {
        
    }

    public void PlugIn(GameObject target)
    {
	    // TODO: make this transform.parent = the target's transform and just set the position to zero
        gameObject.transform.position = target.transform.position + plugOffset;
    }
}
