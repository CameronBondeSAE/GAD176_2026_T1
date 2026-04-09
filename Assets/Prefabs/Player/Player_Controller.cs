using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
	InputSystem_Actions inputSystemActions;

	public Player_Model player_Model;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		inputSystemActions = new InputSystem_Actions();
		inputSystemActions.Enable();
		inputSystemActions.Player.Enable();
		inputSystemActions.Player.Move.performed += player_Model.Move;
		inputSystemActions.Player.Move.canceled  += player_Model.Move;
		
		inputSystemActions.Player.Look.performed += player_Model.Look;
		inputSystemActions.Player.Look.canceled  += player_Model.Look;
	}

	// private void OnEnable()
	// {
	// 	inputSystemActions.Player.Move.performed += player_Model.Move;
	// 	inputSystemActions.Player.Move.canceled += player_Model.Move;
	// 	
	// 	inputSystemActions.Player.Look.performed += player_Model.Look;
	// 	inputSystemActions.Player.Look.canceled += player_Model.Look;
	// }
	//
	// private void OnDisable()
	// {
	// 	inputSystemActions.Player.Move.performed -= player_Model.Move;
	// 	inputSystemActions.Player.Move.canceled -= player_Model.Move;
	// 	inputSystemActions.Player.Look.performed -= player_Model.Look;
	// 	inputSystemActions.Player.Look.canceled -= player_Model.Look;
	// }
}