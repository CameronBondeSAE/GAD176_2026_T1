using Frank;
using UnityEngine;

public class Plug : MonoBehaviour,IHoldable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool isHeld = false;
    public bool isConnected = false;
    [SerializeField] private Vector3 handsOffset = new Vector3(0, 1, 0);
    
    public void Attach(Transform parent) // for adding to player's hands
    {
        if (isHeld == false)
        {
            gameObject.transform.SetParent(parent);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().isTrigger = true;
            isHeld = true;
        }
        
    }

    public void Detach() // for removing from players hand
    {
        if (isHeld)
        {
            gameObject.transform.SetParent(null);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<BoxCollider>().isTrigger = false;
            isHeld = false;
        }
    }

    public void Use() // for putting the plug in the socket
    {
        FindSocket();
    }

    public void PlugIn(Transform Socket)
    {
        if(isConnected == true)
        {
             gameObject.transform.SetParent(Socket);
        }
       
    }

    public void FindSocket() // finding a 
    {
        if (isHeld == true)
        {
            Physics.SphereCast(gameObject.transform.position, 0.2f, transform.forward, out RaycastHit hit);
            if (hit.transform.gameObject.GetComponent<Socket>() != null)
            {
                isConnected = true;
                PlugIn(hit.transform);
            }
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
