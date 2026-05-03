using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Divij
{
    public class SwitchableLight : MonoBehaviour, IInteractable, IPowered
    {
        public Light light;
        
        public bool isPowered;

        public bool isSwitchedOn = false;

        public bool debugRandomSwitching = false;

        private void Start()
        {
	        CheckPower();
        }

        private void FixedUpdate()
        {
	        if (debugRandomSwitching && Random.value < 0.003f)
	        {
		        SetPowered(true);
		        ToggleSwitch();
	        }
        }

        // This is the interface entry point
        public void Interact()
        {
            ToggleSwitch();
        }

        public void ToggleSwitch()
        {
	        isSwitchedOn = !isSwitchedOn;
	        // Debug.Log("CLICK: SwitchableLight: Toggled = "+isSwitchedOn);

	        CheckPower();
        }

        public void CheckPower()
        {
	        if (light == null)
	        {
		        Debug.LogWarning("Light needs to be assigned");
		        return;
	        }
	        
	        if (isPowered && isSwitchedOn)
		        light.enabled = true;
	        else
		        light.enabled = false;
        }


        public void SetPowered(bool powered)
        {
            isPowered = powered;

            CheckPower();
        }

        public bool GetPowered()
        {
	        return isPowered;
        }
    }
}


