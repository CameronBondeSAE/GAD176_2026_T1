using System;
using Frank;
using UnityEngine;

public class CableEnd : MonoBehaviour, IInteractable
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
        gameObject.transform.position = target.transform.position + plugOffset;
    }
}
