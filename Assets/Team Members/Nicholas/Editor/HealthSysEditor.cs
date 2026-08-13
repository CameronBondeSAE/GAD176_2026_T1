#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HealthSys))]
public class HealthSysEditor : Editor
{
    private int damageAmount = 10;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        HealthSys healthSys = (HealthSys)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Health Debug", EditorStyles.boldLabel);

        if (!Application.isPlaying)
        {
            EditorGUILayout.HelpBox("Enter Play Mode to test health.", MessageType.Info);

            return;
        }

        EditorGUILayout.LabelField("Current Health", healthSys.CurrentValue().ToString());

        EditorGUILayout.LabelField("Max Health", healthSys.MaxValue().ToString());

        float healthPercent = 0f;

        if (healthSys.MaxValue() > 0)
        {
            healthPercent = (float)healthSys.CurrentValue() / healthSys.MaxValue();
        }

        Rect healthBar = GUILayoutUtility.GetRect(18, 18);

        EditorGUI.ProgressBar(healthBar, healthPercent, healthSys.CurrentValue() + " / " + healthSys.MaxValue());

        EditorGUILayout.Space();

        damageAmount = EditorGUILayout.IntField("Damage Amount", damageAmount);

        if (GUILayout.Button("Damage"))
        {
            healthSys.Damage(damageAmount);
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Network Debug", EditorStyles.boldLabel);

        EditorGUILayout.LabelField("Is Spawned", healthSys.IsSpawned.ToString());

        EditorGUILayout.LabelField("Is Server", healthSys.IsServer.ToString());

        EditorGUILayout.LabelField("Is Client", healthSys.IsClient.ToString());

        EditorGUILayout.LabelField("Is Host", healthSys.IsHost.ToString());

        EditorGUILayout.LabelField("Is Owner", healthSys.IsOwner.ToString());

        if (!healthSys.IsSpawned)
        {
            EditorGUILayout.HelpBox(
                "This HealthSys is not spawned on the network. Start Host/Server and make sure the object has a NetworkObject.",
                MessageType.Warning);
        }
        if (GUILayout.Button("Hack Death"))
        {
            healthSys.HackDeath();
        }

        Repaint();
    }
}

#endif