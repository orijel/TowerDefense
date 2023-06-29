using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SplitTravelPath : TravelPathBase
{
    [Header("References")] 
    [SerializeField] private TravelPathBase[] travelPaths;
    
    private readonly Random _randomPathSelector = new();
    
    public override PathPoint GetNextPathPoint()
    {
        return null;
    }

    public override PathPoint GetNextPathPoint(PathPoint currentPoint)
    {
        return null;
    }

    public override int GetPointIndex(PathPoint point)
    {
        return 0;
    }

    public override bool IsLastPointInPath(PathPoint point)
    {
        return true;
    }

    public override bool HasReachedPathPoint(PathPoint point, Transform traveler, float distanceThreshold)
    {
        return true;
    }

    public override TravelPathBase GetNextPath()
    {
        var randomPathIndex = _randomPathSelector.Next(0, travelPaths.Length - 1);
        return travelPaths[randomPathIndex];
    }
}
