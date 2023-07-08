using Assets.ExtendedUnityEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace Assets.ExtendedUnityEditor.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, property.displayName, "Tag attribute must be used with string type.");
                return;
            }

            var value = EditorGUI.TagField(position, property.displayName, property.stringValue);
            property.stringValue = value;
        }
    }
}