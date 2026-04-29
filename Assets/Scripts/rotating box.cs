using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Public variable to control rotation speed from the Unity Editor
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object around its local Y-axis
        // Time.deltaTime ensures the rotation is frame-rate independent
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
}