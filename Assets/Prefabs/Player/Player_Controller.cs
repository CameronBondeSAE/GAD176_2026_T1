using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : MonoBehaviour
{
	// InputSystem_Actions inputSystemActions;

	public Player_Model player_Model;

	public PlayerInput playerInput;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		var actions = playerInput.actions;
		actions.Enable();
		actions["Move"].performed += player_Model.Move;
		actions["Move"].canceled += player_Model.Look;
		actions["Look"].performed += player_Model.Look;
		actions["Look"].canceled += player_Model.Look;
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