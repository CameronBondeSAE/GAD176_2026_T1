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

        private void OnEnable()
        {
            shardPrefabProp = serializedObject.FindProperty("shardPrefab");
            minSpawnTimeProp = serializedObject.FindProperty("minSpawnTime");
            maxSpawnTimeProp = serializedObject.FindProperty("maxSpawnTime");
            spawnOnTransformProp = serializedObject.FindProperty("spawnOnTransform");
            spawnBoxBoundsProp = serializedObject.FindProperty("spawnBoxBounds");
            respawnTypeProp = serializedObject.FindProperty("respawnType");
            groundLayerMaskProp = serializedObject.FindProperty("groundLayerMask");
            obstacleLayerMaskProp = serializedObject.FindProperty("obstacleLayerMask");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(shardPrefabProp);
            EditorGUILayout.PropertyField(minSpawnTimeProp);
            EditorGUILayout.PropertyField(maxSpawnTimeProp);
            EditorGUILayout.PropertyField(spawnOnTransformProp);
            EditorGUILayout.PropertyField(spawnBoxBoundsProp);
            EditorGUILayout.PropertyField(groundLayerMaskProp);
            EditorGUILayout.PropertyField(respawnTypeProp);
            EditorGUILayout.PropertyField(obstacleLayerMaskProp);

            serializedObject.ApplyModifiedProperties();

        }
    }
}

#endif