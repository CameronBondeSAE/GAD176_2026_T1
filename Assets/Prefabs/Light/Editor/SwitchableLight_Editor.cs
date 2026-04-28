using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Divij.SwitchableLight), true)]
public class SwitchableLight_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Divij.SwitchableLight switchableLight = target as Divij.SwitchableLight;

		if (GUILayout.Button("Toggle"))
		{
			switchableLight?.ToggleSwitch();
		}
		if (GUILayout.Button("Force Power On"))
		{
			switchableLight?.SetPowered(true);
		}
	}
}