#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StaminaSys))]
public class StaminaSysEditor : Editor
{
    private int staminaAmount = 10;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        StaminaSys staminaSys = (StaminaSys)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Stamina Debug", EditorStyles.boldLabel);

        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Enter Play Mode to test stamina.", MessageType.Info);

            return;
        }

        EditorGUILayout.LabelField("Current Stamina", staminaSys.CurrentValue().ToString());

        EditorGUILayout.LabelField("Max Stamina", staminaSys.MaxValue().ToString());

        float staminaPercent = 0f;

        if (staminaSys.MaxValue() > 0)
        {
            staminaPercent = (float)staminaSys.CurrentValue() / staminaSys.MaxValue();
        }

        Rect staminaBar = GUILayoutUtility.GetRect(18, 18);

        EditorGUI.ProgressBar(staminaBar, staminaPercent, staminaSys.CurrentValue() + " / " + staminaSys.MaxValue());

        EditorGUILayout.Space();

        staminaAmount = EditorGUILayout.IntField("Stamina Usage", staminaAmount);

        if (GUILayout.Button("Use Stamina"))
        {
            staminaSys.UseStamina(staminaAmount);
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Network Debug", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Is Spawned", staminaSys.IsSpawned.ToString());

        EditorGUILayout.LabelField("Is Server", staminaSys.IsServer.ToString());

        EditorGUILayout.LabelField("Is Client", staminaSys.IsClient.ToString());

        EditorGUILayout.LabelField("Is Host", staminaSys.IsHost.ToString());

        EditorGUILayout.LabelField("Is Owner", staminaSys.IsOwner.ToString());

        if (!staminaSys.IsSpawned)
        {
            EditorGUILayout.HelpBox(
                "This StaminaSys is not spawned on the network. Start Host/Server and make sure the object has a NetworkObject.",
                MessageType.Warning);
        }

        Repaint();
    }
}

#endif