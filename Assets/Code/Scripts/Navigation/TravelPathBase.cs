using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TravelPathBase : MonoBehaviour
{
    public virtual bool HasReachedPathPoint(PathPoint point, Transform traveler, float distanceThreshold)
    {
        return Vector2.Distance(traveler.position, point.transform.position) < distanceThreshold;
    }

    public abstract PathPoint GetNextPathPoint();
    public abstract PathPoint GetNextPathPoint(PathPoint currentPoint);
    public abstract int GetPointIndex(PathPoint point);
    public abstract bool IsLastPointInPath(PathPoint point);
    public abstract TravelPathBase GetNextPath();
    public abstract void DrawPath();

    private void OnDrawGizmosSelected()
    {
        DrawPath();
    }
}
