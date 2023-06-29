using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TravelPathBase[] travelPaths;

    private readonly int currentPathIndex = 0;
    
    public TravelPathBase CurrentPath => travelPaths[currentPathIndex];
}
