using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    
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
        _currentPath = _levelManager.CurrentPath;
        _target = _currentPath.GetNextPathPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentPath.HasReachedPathPoint(_target, transform, distanceThreshold))
        {
            if (_currentPath.IsLastPointInPath(_target))
            {
                _currentPath = _currentPath.GetNextPath();
                _target = _currentPath.GetNextPathPoint();
            }
            else
            {
                _target = _currentPath.GetNextPathPoint(_target);
            }
        }

        // TODO: fix for split path
        if (_target.IsEndPoint)
        {
            // TODO: damage the player
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        rb.velocity = direction * (moveSpeed * Time.fixedDeltaTime);
    }
}
