using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyMovement movement;
    
    private EnemySpawner _spawner;

    private void OnReset(Vector3 startingPosition, EnemySpawner spawner)
    {
        _spawner = spawner;
        movement.InitPath();
        transform.position = startingPosition;
    }

    public void Despawn()
    {
        if (_spawner)
        {
            _spawner.Despawn(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public class Factory : MonoMemoryPool<Vector3, EnemySpawner, Enemy>
    {
        protected override void Reinitialize(Vector3 startingPosition, EnemySpawner spawner, Enemy enemy)
        {
            enemy.OnReset(startingPosition, spawner);
            base.Reinitialize(startingPosition, spawner, enemy);
        }
    }
}