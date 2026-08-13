#if UNITY_EDITOR

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Keegan.ShardSpawn
{
    [UnityEditor.CustomEditor(typeof(ShardSpawnController))]
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
            
            EditorGUILayout.LabelField("Spawn timing",  EditorStyles.boldLabel);
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
            EditorGUILayout.LabelField("Base Spawn Settings");
            EditorGUILayout.PropertyField(spawnOnTransformProp);
            EditorGUILayout.PropertyField(spawnBoxBoundsProp);
            EditorGUILayout.PropertyField(groundLayerMaskProp);
            EditorGUILayout.PropertyField(respawnTypeProp);
            EditorGUILayout.PropertyField(obstacleLayerMaskProp);

            if (Application.isPlaying)
            {
                string spawnEnabledStr = shardSpawnEnabledProp.boolValue ? "ON" : "OFF";
                EditorGUILayout.LabelField($"Is Enabled: {spawnEnabledStr}");
            }
            serializedObject.ApplyModifiedProperties();

        }
    }
}

#endif