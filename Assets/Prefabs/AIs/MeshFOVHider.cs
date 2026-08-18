using System;
using Keegan.FOV;
using UnityEngine;

public class MeshFOVHider : MonoBehaviour, IFovDetectable
{
	private void Awake()
	{
		ChangeState(false);	
	}

	public void SetDetected(bool detected)
	{
		ChangeState(detected);
	}

	private void ChangeState(bool detected)
	{
		MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

		foreach (MeshRenderer meshRenderer in meshRenderers)
		{
			meshRenderer.enabled = detected;
		}

		FOVDetection[] fovDetections = GetComponentsInChildren<FOVDetection>();
		foreach (FOVDetection fovDetection in fovDetections)
		{
			fovDetection.enabled = detected;
		}
	}
}
