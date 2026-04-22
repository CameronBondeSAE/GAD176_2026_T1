using UnityEngine;

public class bulletMovement : MonoBehaviour
{
    public float speed = 6f;
    [SerializeField] private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     rb.AddRelativeForce(Vector3.forward * speed);    
    }
    
}
