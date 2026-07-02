using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class SprintingTest : MonoBehaviour
{
    public Rigidbody playerBody;
    private Vector3 velocity;
    public float maxSpeed = 1f;
    private bool maxVelocitySet = false;
    private Vector3 velocityMax;
    
    void FixedUpdate()
    {
            if (Keyboard.current.shiftKey.isPressed)
            {
                    
            }
    }

    
}
