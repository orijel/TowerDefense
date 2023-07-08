using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int initialSpawnedEnemies = 2;
    [SerializeField] private float maxSpawnTime = 3;
    [SerializeField] private float minSpawnTime = 0.5f;
    [SerializeField] private float upperSpawnTimeLimit = 0.8f;
    [SerializeField] private float lowerSpawnTimeLimit = 0.1f;
    [SerializeField] private Transform spawnPoint;
    
    private Enemy.Factory _enemyFactory;
    private float _spawnTimer = 0;
    private float _nextSpawnTime = 0;

    [Inject]
    private void Construct(Enemy.Factory enemyFactory)
    {
        _enemyFactory = enemyFactory;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < initialSpawnedEnemies; i++)
        {
            _enemyFactory.Spawn(spawnPoint.position);
        }
        _nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        if (!(_spawnTimer >= _nextSpawnTime)) return;
        
        _enemyFactory.Spawn(spawnPoint.position);
        _spawnTimer = 0;
        _nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    public void IncreaseDifficulty(float difficultyExponentialModifier)
    {
        maxSpawnTime = maxSpawnTime <= upperSpawnTimeLimit
            ? upperSpawnTimeLimit
            : maxSpawnTime * difficultyExponentialModifier;
        minSpawnTime = minSpawnTime <= lowerSpawnTimeLimit
            ? minSpawnTime
            : minSpawnTime * difficultyExponentialModifier;
    }
}
