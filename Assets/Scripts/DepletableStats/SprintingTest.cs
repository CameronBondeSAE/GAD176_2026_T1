using UnityEngine;
using UnityEngine.InputSystem;
public class SprintingTest : MonoBehaviour
{
    public Rigidbody playerBody;
    
 
    void Update()
    {
        if (Keyboard.current.shiftKey.wasPressedThisFrame)
        {
            playerBody.AddForce(Vector3.Magnitude() * 10000f);
        }
    }
}
