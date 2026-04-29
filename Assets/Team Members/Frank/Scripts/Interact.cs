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
			Collider[] colliders =
				Physics.OverlapBox(transform.position + transform.TransformDirection(Vector3.forward) * 1f,
					new Vector3(0.2f, 1f, 0.75f), transform.rotation);

			foreach (Collider c in colliders)
			{
				// Interact with things
				if (c != null) // primary check - did I hit something - more specifically is there a transform
				{
					Debug.Log("What I hit : " + c.transform.gameObject.name);

					IHoldable holdable = c.transform.GetComponentInParent<IHoldable>();

					if (holdable != null)
					{
						Debug.Log("What I hit : " + c.transform.gameObject.name);
						if (heldGameObject == null)
						{
							if (holdable !=
							    null) // if so then get the gameobject and if it has an IHoldable component, then do the following
							{
								holdable.Pickup(handsTransform);

								if (c.GetComponent<Rigidbody>() != null)
								{
									c.GetComponent<Rigidbody>().isKinematic = true;
								}
								
								// Snap to hands
								c.transform.parent = handsTransform;
								c.transform.position = handsTransform.position;
								heldGameObject = c.transform.gameObject;
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

		private void Drop()
		{
			heldGameObject.GetComponentInParent<IHoldable>().Drop();
			heldGameObject.transform.parent = null;
			
			if (heldGameObject.GetComponent<Rigidbody>() != null)
			{
				heldGameObject.GetComponent<Rigidbody>().isKinematic = false;
			}

			heldGameObject = null;
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
							if (heldGameObject.GetComponent<IHoldable>().YoureBeingHeldButThePlayerJustInteractedWithoutSomethingElse(
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