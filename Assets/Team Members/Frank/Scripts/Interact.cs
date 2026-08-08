using System;
using UnityEngine;
using UnityEngine.XR;

namespace Frank
{
	public class Interact : MonoBehaviour
	{
		[SerializeField]
		private Vector3 Hands = new Vector3(0, 0, 0);

		public Transform handsTransform;
		public GameObject heldGameObject;
		public GameObject cableRef;
		public GameObject powerCableRef;


		public void Pickup()
		{
			// Check whatever is in front
			Collider[] colliders =
				Physics.OverlapBox(transform.position + transform.TransformDirection(Vector3.forward) * 1f,
					new Vector3(0.2f, 1f, 0.75f), transform.rotation, Int32.MaxValue, QueryTriggerInteraction.Ignore);

			// Check each thing
			foreach (Collider c in colliders)
			{
				// Interact with things
				if (c != null) // primary check - did I hit something - more specifically is there a transform
				{
					Debug.Log("What I hit : " + c.transform.gameObject.name);

					IPickup pickup = c.transform.GetComponentInParent<IPickup>();

					if (pickup != null)
					{
						Debug.Log("What I hit : " + c.transform.gameObject.name);
						if (heldGameObject == null)
						{
							if (pickup !=
							    null) // if so then get the gameobject and if it has an IHoldable component, then do the following
							{
								TryPickup(c.transform.gameObject);
							}
						}
						else if (c.transform != null && heldGameObject != null)
						{
							Drop();
						}
					}
				}
			}
		}

		/// <summary>Attempts to pick up one explicit object. Useful for both player and AI callers.</summary>
		public bool TryPickup(GameObject target)
		{
			if (target == null || handsTransform == null || heldGameObject != null)
				return false;

			IPickup pickup = target.GetComponentInParent<IPickup>() ?? target.GetComponentInChildren<IPickup>();
			if (pickup == null)
				return false;

			Component pickupComponent = pickup as Component;
			GameObject pickupObject = pickupComponent != null ? pickupComponent.gameObject : target;
			Rigidbody targetBody = pickupObject.GetComponent<Rigidbody>() ?? target.GetComponentInParent<Rigidbody>();

			pickup.Pickup(handsTransform);
			if (targetBody != null)
			{
				targetBody.linearVelocity = Vector3.zero;
				targetBody.angularVelocity = Vector3.zero;
				targetBody.isKinematic = true;
			}

			pickupObject.transform.SetParent(handsTransform, false);
			pickupObject.transform.localPosition = Vector3.zero;
			pickupObject.transform.localRotation = Quaternion.identity;
			heldGameObject = pickupObject;
			return true;
		}

		public bool TryDrop(Vector3 worldPosition)
		{
			if (heldGameObject == null)
				return false;

			heldGameObject.GetComponentInParent<IPickup>().Drop();
			heldGameObject.transform.SetParent(null, true);
			heldGameObject.transform.position = worldPosition;
			
			if (heldGameObject.GetComponent<Rigidbody>() != null)
			{
				heldGameObject.GetComponent<Rigidbody>().isKinematic = false;
			}

			heldGameObject = null;
			return true;
		}

		private void Drop()
		{
			if (heldGameObject != null)
				TryDrop(heldGameObject.transform.position);
		}

		public void InteractWith()
		{
			Collider[] colliders =
				Physics.OverlapBox(transform.position + transform.TransformDirection(Vector3.forward) * 1.5f,
					new Vector3(0.2f, 1f, 1f), transform.rotation);

			foreach (Collider c in colliders)
			{
				// Interact with things
				if (c != null) // primary check - did I hit something - more specifically is there a transform
				{
					Debug.Log("What I hit : " + c.transform.gameObject.name);


					IInteractable interactable = c.transform.GetComponentInParent<IInteractable>();
					
					if (interactable != null)
					{
						if (heldGameObject != null)
						{
							// Tell the object what we just interacted with
							// Check if it wants to be dropped
							if (heldGameObject.GetComponent<IPickup>().YoureBeingHeldButThePlayerJustInteractedWithSomethingElse(
								    interactable))
							{
								// Item said it's dealing with it, so drop it
								Drop();
							}
							else
							{
								interactable.Interact();
							}
						}
						else
							interactable.Interact();

						
						// if (c.transform.GetComponent<PowerSocket>() != null)
						// {
						// 	if (isHolding == true)
						// 	{
						// 		heldObject.GetComponent<CableEnd>().PlugIn(c.transform.gameObject);
						// 	}
						// }
						// else if (c.transform.GetComponent<PowerPoint>() != null)
						// {
						// 	powerCableRef = Instantiate(cableRef, handsTransform.position, Quaternion.identity);
						// 	powerCableRef.GetComponent<CableManager>()
						// 		.SetReferences(c.transform, handsTransform);
						// 	isHolding = true;
						// 	Debug.Log(isHolding);
						//
						// 	// finds the CableManager component on the instantiated power cable.
						// 	// It passes in a transform for the PowerPoint and one for the player's hands.
						// }
					}
				}
			}
		}
	}
}
