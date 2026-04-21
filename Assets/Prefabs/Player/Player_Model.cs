using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Model : MonoBehaviour
{
	public Rigidbody rb;
	public float     speed;
	public float     turnSpeed = 10f;

	public Vector3 moveDirection;
	Quaternion     LookXZRotation;
	
	public void Look(Vector2 lookDirection)
    {
	    Debug.Log("Look direction = "+lookDirection);

	    Vector3 XZDirection = new Vector3(lookDirection.x,0,lookDirection.y);
	    if(XZDirection.sqrMagnitude > 0.01f) // Avoid looking at ZERO
		    LookXZRotation = Quaternion.LookRotation(XZDirection, Vector3.up);
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
	    Vector2 readValue = callbackContext.ReadValue<Vector2>();
	    Vector3 XZDirection = new Vector3(readValue.x,0,readValue.y);
	    Debug.Log("Move direction = "+readValue.ToString());
	    Debug.Log("		- Phase = " + callbackContext.phase.ToString());
	    
	    moveDirection = XZDirection;
    }

    private void FixedUpdate()
    {
	    Quaternion rbRotation = Quaternion.Slerp(rb.rotation, LookXZRotation,  Time.deltaTime * turnSpeed);
	    if(rbRotation != Quaternion.identity) // HACK checking for zero but this is bad
	    {
		    rb.rotation = rbRotation.normalized;
		    rb.AddForce(moveDirection * speed * Time.deltaTime, ForceMode.VelocityChange);
	    }
    }
}
