using UnityEditor;
using UnityEngine;

namespace PoolAttendant.Editor
{
    [CustomPropertyDrawer(typeof(DefaultPoolItem))]
    public class DefaultPoolItemDrawer : PropertyDrawer
    {
        private SerializedProperty prefab;
        private SerializedProperty size;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect pos, SerializedProperty property, GUIContent label)
        {
            prefab = property.FindPropertyRelative("prefab");
            size = property.FindPropertyRelative("size");

            EditorGUI.BeginProperty(pos, label, property);

            EditorGUI.PropertyField(
                new Rect(pos.x, pos.y, 40, pos.height), size, GUIContent.none);
            
            prefab.objectReferenceValue = EditorGUI.ObjectField(
                new Rect(pos.x + 40, pos.y, pos.width - 40, pos.height), string.Empty,
                prefab.objectReferenceValue, typeof(GameObject), false);

            EditorGUI.EndProperty();
        }
    }
}
