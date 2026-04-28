using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Divij.SwitchableLight), true)]
public class SwitchableLight_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Toggle"))
		{
			Divij.SwitchableLight switchableLight = target as Divij.SwitchableLight;
			if (switchableLight != null) switchableLight.ToggleSwitch();
		}
	}
}
