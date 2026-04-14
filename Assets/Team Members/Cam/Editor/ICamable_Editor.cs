using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Camable), true)]
public class ICamable_Editor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		ICamable camable;
		camable = target as ICamable;

		if (camable != null && GUILayout.Button("GET BIG"))
		{
			// ‘target’ is the magic variable that editors use to link back to the original component. It’s in the BASE CLASS, so you have to ‘cast’ to get access to YOUR functions.
			camable?.TouchCam();
		}
	}
}
