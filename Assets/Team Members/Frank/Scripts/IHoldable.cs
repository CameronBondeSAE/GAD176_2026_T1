using UnityEngine;

namespace Frank
{
	public interface IHoldable
    {
        public void Pickup(Transform parent)
        {
            
        }

        public void Drop()
        {
            
        }

        /// <summary>
        /// This is for eg, power cable being plugged in to a socket
        /// </summary>
        /// <returns>true should drop me, false means it doesn't have any special meaning</returns>
        public bool YoureBeingHeldButThePlayerJustInteractedWithoutSomethingElse(IInteractable interactable)
        {
	        return false;
        }
    }
}


