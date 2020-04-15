using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Pool.Editor
{
    [CustomPropertyDrawer(typeof(DefaultPoolItem))]
    public class DefaultPoolItemDrawer : PropertyDrawer
    {
        private SerializedProperty prefab;
        private SerializedProperty size;

        private const int Spacing = 2;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using (new EditorGUI.PropertyScope(position, label, property)) {
                
                property.serializedObject.Update();
                
                prefab = property.FindPropertyRelative("prefab");
                size = property.FindPropertyRelative("size");
                
                EditorGUI.PropertyField(
                    new Rect(position.x, position.y, 40, position.height), size, GUIContent.none);
            
                prefab.objectReferenceValue = EditorGUI.ObjectField(
                    new Rect(position.x + 40 + Spacing, position.y, position.width - 40 - Spacing, position.height), string.Empty,
                    prefab.objectReferenceValue, typeof(GameObject), false);
                    
                    
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}