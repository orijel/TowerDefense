using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.DI;
using UnityEngine;
using Zenject;
using Random = System.Random;

public class SplitTravelPath : TravelPathBase
{
    [Header("References")] 
    [SerializeField] private TravelPathBase[] travelPaths;
    
    private readonly Random _randomPathSelector = new();
    private PathPoint _dummyPoint;

    [Inject]
    private void Construct([Inject(Id = InjectionId.DummyPathPointId)] PathPoint dummyPathPoint)
    {
        _dummyPoint = dummyPathPoint;
    }
    
    public override PathPoint GetNextPathPoint()
    {
        return _dummyPoint;
    }

    public override PathPoint GetNextPathPoint(PathPoint currentPoint)
    {
        return _dummyPoint;
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
        var randomPathIndex = _randomPathSelector.Next(0, travelPaths.Length);
        return travelPaths[randomPathIndex];
    }
    
    public override void DrawPath()
    {
        foreach (var path in travelPaths)
        {
            path.DrawPath();
        }
    }
}
