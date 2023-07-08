using Assets.ExtendedUnityEditor.Runtime;
using UnityEditor;
using UnityEngine;

namespace Assets.ExtendedUnityEditor.Editor
{
    [CustomPropertyDrawer(typeof(LayerAttribute))]
    public class LayerAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                EditorGUI.LabelField(position, property.displayName, "Layer attribute must be used with integer type.");
                return;
            }

            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}