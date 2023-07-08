using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.ExtendedUnityEditor.Editor
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class TagInspector : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var myInspector = new VisualElement();

            InspectorElement.FillDefaultInspector(myInspector, serializedObject, this);

            return myInspector;
        }
    }
}
