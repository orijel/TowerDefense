using System;
using System.Collections;
using System.Collections.Generic;
using Assets.ExtendedUnityEditor.Runtime;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Collision2D_Handler : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<Collision2D> collisionEnter;
    [SerializeField] private UnityEvent<Collision2D> collisionExit;
    [SerializeField] private UnityEvent<Collider2D> triggerEnter;
    [SerializeField] private UnityEvent<Collider2D> triggerExit;
    
    [Header("Collision Settings")]
    [SerializeField] private bool shouldCheckLayerMask;
    [Tooltip("Layer mask of colliding object to check against, will be ignored if should Check Layer Mask is disabled")]
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool shouldCheckTag;
    [Tooltip("Tag of colliding object to check against, will be ignored if should Check Tag is disabled")]
    [SerializeField, Tag] private string compareTag;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!ShouldCollideWithObject(other.gameObject)) return;
        collisionEnter.Invoke(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!ShouldCollideWithObject(other.gameObject)) return;
        collisionExit.Invoke(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!ShouldCollideWithObject(other.gameObject)) return;
        triggerEnter.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!ShouldCollideWithObject(other.gameObject)) return;
        triggerExit.Invoke(other);
    }
    
    private void OnDrawGizmos()
    {
        var triggerCollider = GetComponent<Collider2D>();
        var bounds = triggerCollider.bounds;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
    
    private bool ShouldCollideWithObject(GameObject other)
    {
        return IsCollidedObjectInMask(other) && IsCollidedObjectWithTag(other);
    }
    
    private bool IsCollidedObjectWithTag(GameObject other)
    {
        return !shouldCheckTag || other.CompareTag(compareTag);
    }

    private bool IsCollidedObjectInMask(GameObject other)
    {
        return !shouldCheckLayerMask || LayerUtils.IsLayerInMask(mask, other.layer);
    }
}
