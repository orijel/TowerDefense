using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyMovement movement;
    
    private Factory _spawner;

    private void OnReset(Vector3 startingPosition)
    {
        movement.InitPath();
        transform.position = startingPosition;
    }
    
    private void SetSpawner(Factory spawner)
    {
        _spawner = spawner;
    }

    public void Despawn()
    {
        if (_spawner != null)
        {
            _spawner.Despawn(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public class Factory : MonoMemoryPool<Vector3, Enemy>
    {
        protected override void Reinitialize(Vector3 startingPosition, Enemy enemy)
        {
            enemy.OnReset(startingPosition);
            base.Reinitialize(startingPosition, enemy);
        }

        protected override void OnCreated(Enemy enemy)
        {
            enemy.SetSpawner(this);
            base.OnCreated(enemy);
        }
    }
}