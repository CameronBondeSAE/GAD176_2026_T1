using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Model : MonoBehaviour
{
	public Rigidbody rb;
	public float     speed;
	public float     turnSpeed = 10f;

	public Vector3 direction;
	Quaternion     LookXZRotation;
	
	public void Look(InputAction.CallbackContext callbackContext)
    {
	    Vector2 readValue = callbackContext.ReadValue<Vector2>();
	    Debug.Log("Look direction = "+readValue.ToString());
	    Debug.Log("		- Phase = " + callbackContext.phase.ToString());

	    Vector3 XZDirection = new Vector3(readValue.x,0,readValue.y);
	    if(XZDirection.sqrMagnitude > 0.01f) // Avoid looking at ZERO
		    LookXZRotation = Quaternion.LookRotation(XZDirection, Vector3.up);
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
	    Vector2 readValue = callbackContext.ReadValue<Vector2>();
	    Vector3 XZDirection = new Vector3(readValue.x,0,readValue.y);
	    Debug.Log("Move direction = "+readValue.ToString());
	    Debug.Log("		- Phase = " + callbackContext.phase.ToString());
	    
	    direction = XZDirection;
    }

    private void FixedUpdate()
    {
	    rb.rotation = Quaternion.Slerp(rb.rotation, LookXZRotation,  Time.deltaTime * turnSpeed);
	    rb.AddForce(direction * speed * Time.deltaTime, ForceMode.VelocityChange);
    }
}
