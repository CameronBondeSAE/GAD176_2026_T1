using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
	// InputSystem_Actions inputSystemActions;

	public Player_Model player_Model;

	public PlayerInput playerInput;

	public Vector2 mousePosition;

	public bool usingMouse = false;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		var actions = playerInput.actions;
		actions.Enable();
		actions["Move"].performed += player_Model.Move;
		actions["Move"].canceled += player_Model.Move;
		actions["Look"].performed += Look;
		actions["Look"].canceled += Look;
		actions["Mouse Position"].performed += UpdateMousePosition;
		actions["Mouse Position"].canceled += UpdateMousePosition;
	}

	private void Look(InputAction.CallbackContext obj)
	{
		usingMouse = false;
		ResolveLookDirection(obj);
	}

	private void UpdateMousePosition(InputAction.CallbackContext obj)
	{
		usingMouse = true;
		mousePosition = obj.ReadValue<Vector2>();
		// Debug.Log("Mouse Position: " + mousePosition);
		ResolveLookDirection(obj);
	}

	private void ResolveLookDirection(InputAction.CallbackContext callbackContext)
	{
		// Gamepad bound to Look action takes priority when there's actual input
		if (!usingMouse)
		{
			var stickInput = callbackContext.ReadValue<Vector2>();
			// if (stickInput.sqrMagnitude > 0.01f)
			player_Model.moveDirection = stickInput.normalized;
		}
		else
		{
			player_Model.Look(GetMouseLookDirection());
		}
	}

	private Vector2 GetMouseLookDirection()
	{
		if (Camera.main != null)
		{
			var ray = Camera.main.ScreenPointToRay(mousePosition);

			var groundPlane = new Plane(Vector3.up, transform.position);
			if (!groundPlane.Raycast(ray, out float distance))
				return Vector2.zero;

			var worldPoint = ray.GetPoint(distance);
			var offset = worldPoint - transform.position;

			// Flatten to XZ and normalize → Vector2(x, z)
			var dir = new Vector2(offset.x, offset.z);
			return dir.sqrMagnitude > 0.001f ? dir.normalized : Vector2.zero;
		}
		else
		{
			Debug.LogWarning("No Camera");
			return Vector2.zero;
		}
	}
}