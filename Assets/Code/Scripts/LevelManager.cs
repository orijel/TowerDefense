using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [Header("Enemies")] 
    [SerializeField][Range(0.1f, 1)] private float difficultyModifier = 0.9f;
    [SerializeField] private float difficultyIncreaseTime = 15f;
    
    [Header("References")]
    [SerializeField] private TravelPathBase startingPath;
    

    private EnemySpawner _enemySpawner;
    private float _currentDifficultyIncreaseTime = 0f;

    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    private void Update()
    {
        _currentDifficultyIncreaseTime += Time.deltaTime;
        if (_currentDifficultyIncreaseTime >= difficultyIncreaseTime)
        {
            _currentDifficultyIncreaseTime = 0;
            _enemySpawner.IncreaseDifficulty(difficultyModifier);
        }
    }

    public TravelPathBase StartingPath => startingPath;
}
