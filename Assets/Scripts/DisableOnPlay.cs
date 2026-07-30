using System;
using UnityEditor;
using UnityEngine;

public class DisableOnPlay : MonoBehaviour
{
	private void Awake()
	{
		EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
	}

	private void EditorApplicationOnplayModeStateChanged(PlayModeStateChange obj)
	{
		if (obj == PlayModeStateChange.EnteredPlayMode)
		{
			foreach (Transform componentsInChild in transform.GetComponentsInChildren<Transform>())
			{
				componentsInChild.gameObject.SetActive(false);
			}
		}
	}
}
