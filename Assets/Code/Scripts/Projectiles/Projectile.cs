using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Framework.Health;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour, IDamageApplier
{
    [SerializeField] private float velocityMultiplier = 10f;
    [SerializeField] private LayerMask enemyLayerMask;
    [SerializeField] private float damage = 1f;
    
    private Factory _spawner;
    private Rigidbody2D _rigidbody;
    private bool _hasCollided = false;

    public float Damage => damage;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnReset(Vector3 startingPosition)
    {
        _hasCollided = false;
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

    public void OnCollisionWithObject(Collider2D other)
    {
        if (_hasCollided) return;
        _hasCollided = true;
        
        if (LayerUtils.IsLayerInMask(enemyLayerMask, other.gameObject.layer))
        {
            var hittable = other.GetComponent<IDamageTaker>();
            hittable.TakeDamage(this);
        }
        
        Despawn();
    }
    
    public void FireProjectile(Vector3 slingshotPosition)
    {
        var direction = (slingshotPosition - transform.position).normalized;
        _rigidbody.velocity = direction * velocityMultiplier;
    }
    
    public class Factory : MonoMemoryPool<Vector3, Projectile>
    {
        protected override void Reinitialize(Vector3 startingPosition, Projectile projectile)
        {
            projectile.OnReset(startingPosition);
            base.Reinitialize(startingPosition, projectile);
        }

        protected override void OnCreated(Projectile projectile)
        {
            projectile.SetSpawner(this);
            base.OnCreated(projectile);
        }
    }
}
