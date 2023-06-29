using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelPath : TravelPathBase
{
    [Header("References")]
    [SerializeField] private PathPoint[] pathPoints;
    [SerializeField] private TravelPathBase nextPath;

    private void Start()
    {
        foreach (var pathPoint in pathPoints)
        {
            pathPoint.Init(this);
        }
    }
    
    public override PathPoint GetNextPathPoint()
    {
        return pathPoints[0];
    }
    
    public override PathPoint GetNextPathPoint(PathPoint currentPoint)
    {
        var nextIndex = currentPoint.Index + 1;
        return nextIndex >= pathPoints.Length ? nextPath.GetNextPathPoint() : pathPoints[nextIndex];
    }

    public override int GetPointIndex(PathPoint point)
    {
        return Array.IndexOf(pathPoints, point);
    }

    public override bool IsLastPointInPath(PathPoint point)
    {
        return point.Index == pathPoints.Length - 1;
    }

    public override TravelPathBase GetNextPath()
    {
        return nextPath;
    }
}
