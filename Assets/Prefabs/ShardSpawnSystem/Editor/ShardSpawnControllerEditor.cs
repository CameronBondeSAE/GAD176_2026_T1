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
        private SerializedProperty loopSpawnProp;
        private SerializedProperty spawnOnTransformProp;
        private SerializedProperty spawnBoxBoundsProp;

        private void OnEnable()
        {
            shardPrefabProp = serializedObject.FindProperty("shardPrefab");
            minSpawnTimeProp = serializedObject.FindProperty("minSpawnTime");
            maxSpawnTimeProp = serializedObject.FindProperty("maxSpawnTime");
            loopSpawnProp = serializedObject.FindProperty("loopSpawn");
            spawnOnTransformProp = serializedObject.FindProperty("spawnOnTransform");
            spawnBoxBoundsProp = serializedObject.FindProperty("spawnBoxBounds");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(shardPrefabProp);
            EditorGUILayout.PropertyField(minSpawnTimeProp);
            EditorGUILayout.PropertyField(maxSpawnTimeProp);
            EditorGUILayout.PropertyField(loopSpawnProp);
            EditorGUILayout.PropertyField(spawnOnTransformProp);
            EditorGUILayout.PropertyField(spawnBoxBoundsProp);

            serializedObject.ApplyModifiedProperties();

        }
    }
}

#endif