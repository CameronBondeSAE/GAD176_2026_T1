using System;
using UnityEngine;
using UnityEngine.XR;

namespace Frank
{
	public class Interact : MonoBehaviour
	{
		public KeyCode keyCode = KeyCode.Space;
		public KeyCode pickupKey = KeyCode.G;
		public KeyCode useKey = KeyCode.H;
		public Transform headTransform;

		[SerializeField]
		private Vector3 Hands = new Vector3(0, 0, 0);

		public bool isHolding = false;
		public Transform HandsTransform;
		public GameObject heldObject;
		public GameObject heldItem;
		public GameObject cableRef;
		public GameObject powerCableRef;

		// Update is called once per frame
		void Update()
		{
			//if (isHolding == true)
			// {
			//heldObject.transform.position = headTransform.position + Hands;
			// }

			if (Input.GetKeyDown(keyCode)) // if key pressed is space then do the following
			{
				Collider[] colliders = Physics.OverlapBox(transform.position + transform.TransformDirection(Vector3.forward) * 1.5f, new Vector3(0.2f, 1f, 1f), transform.rotation);

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
								powerCableRef = Instantiate(cableRef, HandsTransform.position, Quaternion.identity);
								powerCableRef.GetComponent<CableManager>()
									.SetReferences(c.transform, HandsTransform);
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

			else if (Input.GetKeyDown(pickupKey))
			{
				Ray ray = new Ray();
				ray.origin = headTransform.position;
				ray.direction = headTransform.forward;
				RaycastHit hitInfo = new RaycastHit();
				Physics.Raycast(ray, out hitInfo, 3f);

				if (hitInfo.transform != null && isHolding == false)
				{
					Debug.Log("What I hit : " + hitInfo.transform.gameObject.name);
					Debug.Log("    Distance to thing I hit : " + hitInfo.distance);
					Debug.Log("    Where I hit : " + hitInfo.point);

					if (hitInfo.transform.GetComponentInParent<IHoldable>() !=
					    null) // if so then get the gameobject and if it has an IHoldable component, then do the following
					{
						hitInfo.transform.GetComponentInParent<IHoldable>().Pickup(headTransform);
						isHolding = true;
						heldItem = hitInfo.transform.gameObject;
					}
				}

				else if (hitInfo.transform != null && isHolding)
				{
					if (Input.GetKeyDown(pickupKey))
					{
						hitInfo.transform.GetComponentInParent<IHoldable>().Drop();
						isHolding = false;
					}
				}
			}

			else if (Input.GetKeyDown(useKey) && isHolding)
			{
				//heldItem.GetComponent<Plug>().Use();
			}
		}
	}
}