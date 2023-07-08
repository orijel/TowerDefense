using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float velocityMultiplier = 10f;
    
    private Factory _spawner;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void OnReset(Vector3 startingPosition)
    {
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

    public void OnCollisionWithEdge()
    {
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
