using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Enemy enemy;
    
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float distanceThreshold = 0.1f;

    private LevelManager _levelManager;
    private PathPoint _target;
    private TravelPathBase _currentPath;

    [Inject]
    private void Construct(LevelManager manager)
    {
        _levelManager = manager;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_currentPath.HasReachedPathPoint(_target, transform, distanceThreshold)) return;
        if (_currentPath.IsLastPointInPath(_target))
        {
            if (_target.IsEndPoint)
            {
                // TODO: damage the player
                enemy.Despawn();
                return;
            }
                
            _currentPath = _currentPath.GetNextPath();
            _target = _currentPath.GetNextPathPoint();
        }
        else
        {
            _target = _currentPath.GetNextPathPoint(_target);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        rb.velocity = direction * (moveSpeed * Time.fixedDeltaTime);
    }
    
    public void InitPath()
    {
        _currentPath = _levelManager.StartingPath;
        _target = _currentPath.GetNextPathPoint();
    }
}
