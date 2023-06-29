using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    [SerializeField] private bool isEndPoint = false;

    public void Init(TravelPathBase path)
    {
        Index = path.GetPointIndex(this);
    }

    public int Index { get; private set; }
    public bool IsEndPoint => isEndPoint;
}
