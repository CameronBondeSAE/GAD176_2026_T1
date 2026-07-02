using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
public class SprintingTest : MonoBehaviour
{
    public Rigidbody playerBody;
    private Vector3 velocity;
    public float newSpeedMultiplier = 10f;
    private float baseSpeedMultiplier;
    private Player_Model playerModel;
    public StaminaSys staminaSys;
    private bool canSprint = true;

    
    private void OnEnable()
    {
        if (staminaSys == null)
        {
            Debug.LogWarning("NULL: staminaSys is not assigned on player");
            return;
        }
        staminaSys.OnStaminaFullEvent.AddListener(StaminaFull);
        staminaSys.StaminaDepletedEvent.AddListener(StaminaEmpty);
    }

    private void OnDisable()
    {
        staminaSys.OnStaminaFullEvent.RemoveListener(StaminaFull);
        staminaSys.StaminaDepletedEvent.RemoveListener(StaminaEmpty);
    }
    
    
    
    
    private void Start()
    {
        playerModel = GetComponentInChildren<Player_Model>();
        staminaSys = GetComponent<StaminaSys>();
        if (playerModel == null)
        {
            Debug.LogWarning("No Player_Model script found in child gameobjects");
            
        }
        if (staminaSys == null)
        {
            Debug.LogWarning("No StaminaSys script found in root gameobjects");
        }
        else
        {
            baseSpeedMultiplier = GetComponentInChildren<Player_Model>().speedMultiplier;
        }
        
    }

    void FixedUpdate()
    {
       Sprint();
    }

    public void StaminaFull()
    {
        canSprint = true;
    }
    
    public void StaminaEmpty()
    {
        canSprint = false;
    }
    //Bools to tell Sprint() if the player can run.

    public void Sprint()
    {
        if (canSprint == true)
        {
            if (Keyboard.current.shiftKey.isPressed)
            {
                GetComponent<StaminaSys>().OnUse(20);
                GetComponentInChildren<Player_Model>().speedMultiplier = newSpeedMultiplier;
            }
            else
            {
                GetComponentInChildren<Player_Model>().speedMultiplier = baseSpeedMultiplier;
            }
        }
    }
    //If you're pressing shift, start telling the stamina system you're using stamina, set the movespeed mult
    //to whatever the "newSpeedMultiplier" variable is set to (10) by default, thus increasing speed.
    //If shift not pressed, sets the speedmultiplier to old multiplier.
}
