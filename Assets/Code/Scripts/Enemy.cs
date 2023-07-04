using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour, IPoolable<Vector3, IMemoryPool>, IDisposable
{
    private IMemoryPool _pool;

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(Vector3 startingPosition, IMemoryPool pool)
    {
        transform.position = startingPosition;
        _pool = pool;
    }

    public void Dispose()
    {
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<Vector3, Enemy>
    {
    }
}