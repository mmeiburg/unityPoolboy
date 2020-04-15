using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TinyTools.PoolAttendant
{
    [CustomPropertyDrawer(typeof(DefaultPoolItemList))]
    public class ReorableListDefaultPoolItem : PropertyDrawer
    {
        private SerializedProperty listProperty;
        private SerializedObject serializedObject;
        private ReorderableList reorderableList;
        
        private void Initialize(SerializedProperty property)
        {
            if (reorderableList != null) {
                return;
            }

            FindItemsProperty(property);
            
            serializedObject = property.serializedObject;
            
            reorderableList = new ReorderableList(serializedObject, listProperty,  
                false, true, true, true);

            InitializeHeader();
            InitializeItems();
        }
        
        private void InitializeHeader()
        {
            reorderableList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, new GUIContent(listProperty.displayName));
            };
        }

        private void InitializeItems()
        {
            reorderableList.drawElementCallback = (rect, index, active, focused) =>
            {
                SerializedProperty property = listProperty.GetArrayElementAtIndex(index);

                EditorGUI.PropertyField(rect, property);
                serializedObject.ApplyModifiedProperties();
            };
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            reorderableList?.DoLayoutList();
        }
        
        private void FindItemsProperty(SerializedProperty property)
        {
            listProperty = property.FindPropertyRelative("items");
        }
    }
}