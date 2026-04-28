using UnityEngine;

namespace Divij
{
    public class SwitchableLight : MonoBehaviour, IInteractable, IPowered
    {
        public Light light;
        
        public bool isPowered;

        public bool isSwitchedOn = false;

        // This is the interface entry point
        public void Interact()
        {
            ToggleSwitch();
        }

        public void ToggleSwitch()
        {
	        isSwitchedOn = !isSwitchedOn;
	        Debug.Log("CLICK: SwitchableLight: Toggled = "+isSwitchedOn);

	        CheckPower();
        }

        public void CheckPower()
        {
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


