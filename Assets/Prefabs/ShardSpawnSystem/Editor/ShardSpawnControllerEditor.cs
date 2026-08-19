#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace Keegan.ShardSpawn
{
    [CustomEditor(typeof(ShardSpawnController))]
    public class ShardSpawnControllerEditor : Editor
    {
        private SerializedProperty shardPrefabProp;
        private SerializedProperty minSpawnTimeProp;
        private SerializedProperty maxSpawnTimeProp;
        private SerializedProperty spawnOnTransformProp;
        private SerializedProperty spawnBoxBoundsProp;
        private SerializedProperty respawnTypeProp;
        private SerializedProperty groundLayerMaskProp;
        private SerializedProperty obstacleLayerMaskProp;

        private SerializedProperty spawnDuringTimesProp;
        private SerializedProperty shardSpawnFromProp;
        private SerializedProperty shardSpawnToProp;

        private SerializedProperty shardSpawnEnabledProp;
        private SerializedProperty maxInSpawnAreaProp;

        private void OnEnable()
        {
            shardPrefabProp = serializedObject.FindProperty("shardPrefab");
            minSpawnTimeProp = serializedObject.FindProperty("minSpawnTime");
            maxSpawnTimeProp = serializedObject.FindProperty("maxSpawnTime");
            spawnDuringTimesProp = serializedObject.FindProperty("spawnDuringTimeRange");
            shardSpawnFromProp = serializedObject.FindProperty("shardSpawnFrom");
            shardSpawnToProp = serializedObject.FindProperty("shardSpawnTo");
            spawnOnTransformProp = serializedObject.FindProperty("spawnOnTransform");
            spawnBoxBoundsProp = serializedObject.FindProperty("spawnBoxBounds");
            respawnTypeProp = serializedObject.FindProperty("respawnType");
            groundLayerMaskProp = serializedObject.FindProperty("groundLayerMask");
            obstacleLayerMaskProp = serializedObject.FindProperty("obstacleLayerMask");
            shardSpawnEnabledProp = serializedObject.FindProperty("canSpawnShard");
            maxInSpawnAreaProp = serializedObject.FindProperty("maxInSpawnArea");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(shardPrefabProp);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Spawn Timing", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(minSpawnTimeProp);
            EditorGUILayout.PropertyField(maxSpawnTimeProp);
            EditorGUILayout.PropertyField(spawnDuringTimesProp);
            EditorGUILayout.PropertyField(maxInSpawnAreaProp);
            

            if (spawnDuringTimesProp.boolValue)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PropertyField(shardSpawnFromProp);
                EditorGUILayout.PropertyField(shardSpawnToProp);

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Base Spawn Settings", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(spawnOnTransformProp);
            EditorGUILayout.PropertyField(spawnBoxBoundsProp);
            EditorGUILayout.PropertyField(groundLayerMaskProp);
            EditorGUILayout.PropertyField(obstacleLayerMaskProp);
            EditorGUILayout.PropertyField(respawnTypeProp);

            EditorGUILayout.Space();

            if (Application.isPlaying)
            {
                ShardSpawnController controller = (ShardSpawnController)target;
                EditorGUILayout.LabelField("Runtime Debug", EditorStyles.boldLabel);
                string spawnEnabledStr = shardSpawnEnabledProp.boolValue ? "ON" : "OFF";
                EditorGUILayout.LabelField("Shard Spawning", spawnEnabledStr);
                EditorGUILayout.LabelField("Is Server", controller.IsServer.ToString());
                EditorGUILayout.LabelField("Is Spawned", controller.IsSpawned.ToString());
                EditorGUILayout.Space();

                if (GUILayout.Button("Enable Shard Spawning"))
                {
                    shardSpawnEnabledProp.boolValue = true;
                    serializedObject.ApplyModifiedProperties();
                    controller.TriggerShardSpawn();
                }

                if (GUILayout.Button("Disable Shard Spawning"))
                {
                    shardSpawnEnabledProp.boolValue = false;
                    serializedObject.ApplyModifiedProperties();
                }

                EditorGUILayout.Space();

                if (GUILayout.Button("Spawn Shard Now"))
                {
                    controller.DebugSpawnShard();
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}

#endif