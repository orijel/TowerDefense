using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Framework.Health;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IDamageTaker
{
    [Header("References")]
    [SerializeField] private EnemyMovement movement;
    
    [Header("Attributes")]
    [SerializeField] private float health = 2;
    
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
    
    public void TakeDamage(IDamageApplier damageApplier)
    {
        health -= damageApplier.Damage;
        if (health <= 0)
        {
            Despawn();
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