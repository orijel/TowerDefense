using Assets.ExtendedUnityEditor.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Assets.ExtendedUnityEditor.Editor
{
    [CustomPropertyDrawer(typeof(Tag))]
    public class TagPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var container = new VisualElement();
            var tagField =
                new TagField(property.displayName, property.FindPropertyRelative("TagName").stringValue)
                {
                    bindingPath = "TagName"
                };
            container.Add(tagField);
            return container;
        }
    }
}
