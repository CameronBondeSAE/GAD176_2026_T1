using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class SprintingTest : MonoBehaviour
{
    public Rigidbody playerBody;
    private Vector3 velocity;
    
// Try ClampMagnitude
   
    void FixedUpdate()
    {
        if (Keyboard.current.shiftKey.isPressed)
        {
            velocity = playerBody.linearVelocity;
            velocity += velocity;
            playerBody.linearVelocity = velocity;
            Debug.Log("im working");
        }
    }
}
