#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Libs.UnityUtils
{
    [CustomPropertyDrawer(typeof(AssetOnlyAttribute))]
    public class AssetOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            var obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, fieldInfo.FieldType, allowSceneObjects: false);
            if (EditorGUI.EndChangeCheck())
                property.objectReferenceValue = obj;
        }
    }
}
#endif