using Unity.Netcode;
using UnityEngine;

namespace Divij
{
    public class SwitchableLightController : NetworkBehaviour, IInteractable
    {
        [SerializeField] private SwitchableLightModel model;
        
        // This is the interface entry point
        public void Interact()
        {
            model.ToggleSwitch();
        }
    }
}

