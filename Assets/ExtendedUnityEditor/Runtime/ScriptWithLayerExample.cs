using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.ExtendedUnityEditor.Runtime
{
    public class ScriptWithLayerExample : MonoBehaviour
    {
        [SerializeField, Layer] private int _layerAsAttribute;
        [SerializeField, Layer] private string _layerError;
    }
}