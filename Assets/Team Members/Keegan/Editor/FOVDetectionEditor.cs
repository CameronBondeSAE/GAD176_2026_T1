using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Keegan.FOV
{
    [CustomEditor(typeof(FOVDetection))]
    public class FOVDetectionEditor : Editor
    {
        //== LINE RAYCAST SETTINGS ==//
        private SerializedProperty _detectionCastCountProp;
        
        //private SerializedProperty _castDirectionsProp;
        
        //== RADIAL CAST SETTINGS ==//
        private SerializedProperty _totalFovAngleProp;
        
        
        //== BASE CAST SETTINGS ==//
        private SerializedProperty _castType;
        private SerializedProperty _sightCastDistProp;
        private SerializedProperty _detectionMaskProp;
        private SerializedProperty _shapeColorProp;
        private SerializedProperty _visualTypeProp;

        private SerializedProperty _drawDebug;

        private SerializedProperty _seenEnemyProp;
        private SerializedProperty _lostEnemyProp;


        private void OnEnable()
        {
            _detectionCastCountProp = serializedObject.FindProperty("_detectionCastCount");
            _totalFovAngleProp = serializedObject.FindProperty("_totalFovAngle");
            _sightCastDistProp = serializedObject.FindProperty("_sightCastDistance");
            _castType = serializedObject.FindProperty("_castType");
            _detectionMaskProp = serializedObject.FindProperty("_detectionMask");
            _visualTypeProp = serializedObject.FindProperty("_visualType");
            _shapeColorProp = serializedObject.FindProperty("_fovShapeColor");
            _drawDebug = serializedObject.FindProperty("_drawDebug");
            _seenEnemyProp = serializedObject.FindProperty("seenEnemy");
            _lostEnemyProp = serializedObject.FindProperty("lostEnemy");
        }


        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("FOV Detection", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_sightCastDistProp);
            EditorGUILayout.PropertyField(_detectionMaskProp);
            EditorGUILayout.PropertyField(_visualTypeProp);
            EditorGUILayout.PropertyField(_shapeColorProp);
            
            //== CAST TYPE PROPERTIES ==//
            EditorGUILayout.PropertyField(_castType);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            FOVDetection.CastType castType = (FOVDetection.CastType)_castType.enumValueIndex;
            if (castType == FOVDetection.CastType.Line)
            {
                EditorGUILayout.LabelField("Line Cast Settings", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_detectionCastCountProp);
            }
            else if (castType == FOVDetection.CastType.Radial)
            {
                EditorGUILayout.LabelField("Radial Cast Settings",  EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(_totalFovAngleProp);
            }
            
            //== EVENTS ==//
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Events",  EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_seenEnemyProp);
            EditorGUILayout.PropertyField(_lostEnemyProp);
            
            
            //= DEBUG ==//
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            EditorGUILayout.LabelField("Debug Settings", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_drawDebug);
                
            
            // Apply the updated values
            serializedObject.ApplyModifiedProperties();
        }
        
    }
        
}
