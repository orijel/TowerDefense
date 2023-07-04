using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private TravelPathBase[] travelPaths;

    private readonly int currentPathIndex = 0;
    private EnemySpawner _enemySpawner;

    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }
    
    public TravelPathBase CurrentPath => travelPaths[currentPathIndex];
}
