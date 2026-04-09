using UnityEngine;
using Divij;


public class PowerPoint : MonoBehaviour, IInteractable
{
    public GameObject cableRef;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        Debug.Log("Cable in hand");
        Instantiate(cableRef, transform.position + Vector3.forward, Quaternion.identity);
    }
}
