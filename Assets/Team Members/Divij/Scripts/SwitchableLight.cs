using UnityEngine;

namespace Divij
{
    public class SwitchableLight : MonoBehaviour, IInteractable, IPowered
    {
        public Light light;
        
        public bool isPowered;


        // This is the interface entry point
        public void Interact()
        {
            if (!isPowered)
            {
                Debug.Log("No power");
                return;
            }
            
            Toggle();
        }


        public void Toggle()
        {
            Debug.Log("SwitchableLight: Toggle");
            light.enabled = !light.enabled;
        }


        public void SetPowered(bool powered)
        {
            isPowered = powered;

            if (!isPowered && light.enabled)
            {
                light.enabled = false;
            }
        }

        public bool GetPowered()
        {
	        return isPowered;
        }
    }
}


