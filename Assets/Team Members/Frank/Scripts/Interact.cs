using System;
using UnityEngine;
using UnityEngine.XR;

namespace Frank
{
	public class Interact : MonoBehaviour
	{
		[SerializeField]
		private Vector3 Hands = new Vector3(0, 0, 0);

		public bool isHolding = false;
		public Transform handsTransform;
		public GameObject heldObject;
		public GameObject heldItem;
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

					if (c.transform.GetComponentInParent<IHoldable>() != null)
					{
						Debug.Log("What I hit : " + c.transform.gameObject.name);
						if (isHolding == false)
						{
							if (c.transform.GetComponentInParent<IHoldable>() !=
							    null) // if so then get the gameobject and if it has an IHoldable component, then do the following
							{
								c.transform.GetComponentInParent<IHoldable>().Pickup(handsTransform);

								if (c.GetComponent<Rigidbody>() != null)
								{
									c.GetComponent<Rigidbody>().isKinematic = true;
								}
								
								// Snap to hands
								c.transform.parent = handsTransform;
								c.transform.position = handsTransform.position;
								isHolding = true;
								heldItem = c.transform.gameObject;
							}
						}
						else if (c.transform != null && isHolding)
						{
							c.transform.GetComponentInParent<IHoldable>().Drop();
							c.transform.parent = null;
							isHolding = false;
							
							if (c.GetComponent<Rigidbody>() != null)
							{
								c.GetComponent<Rigidbody>().isKinematic = false;
							}
						}
					}
				}
			}
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


					if (c.transform.GetComponentInParent<IInteractable>() != null)
					{
						if (c.transform.GetComponent<PowerPoint>() != null)
						{
							powerCableRef = Instantiate(cableRef, handsTransform.position, Quaternion.identity);
							powerCableRef.GetComponent<CableManager>()
								.SetReferences(c.transform, handsTransform);
							isHolding = true;
							Debug.Log(isHolding);

							// finds the CableManager component on the instantiated power cable.
							// It passes in a transform for the PowerPoint and one for the player's hands.
						}
						else
						{
							c.transform.GetComponentInParent<Divij.IInteractable>().Interact();
						}
					}

					else if (c.transform.GetComponent<Divij.IPowered>() != null)
					{
						if (c.transform.GetComponent<Socket>() != null)
						{
							if (isHolding == true)
							{
								heldObject.GetComponent<CableEnd>().PlugIn(c.transform.gameObject);
							}
						}
					}
				}
			}
		}
	}
}