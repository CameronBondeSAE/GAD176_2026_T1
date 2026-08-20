using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Divij.SwitchableLightModel), true)]
public class SwitchableLight_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		Divij.SwitchableLightModel switchableLightModel = target as Divij.SwitchableLightModel;

		if (GUILayout.Button("Toggle"))
		{
			switchableLightModel?.ToggleSwitch();
		}
		if (GUILayout.Button("Force Power On"))
		{
			switchableLightModel?.SetPowered(true);
		}
	}
}