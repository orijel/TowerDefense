using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

public class Slingshot : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float maxClickCheckDistance = 20f;
    [SerializeField] private LayerMask clickCheckLayerMask;
    
    private SlingshotControls _slingshotControls;
    private bool _isAiming;
    private Projectile.Factory _projectileSpawner;
    private Projectile _activeProjectile;

    [Inject]
    private void Construct(Projectile.Factory projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
    }

    void Awake()
    {
        _slingshotControls = new SlingshotControls();
        var aimAction = _slingshotControls.Main.Aim;
        aimAction.performed += OnAimPerformed;
        aimAction.canceled += OnAimCanceled;
    }

    private void OnEnable()
    {
        _slingshotControls.Main.Enable();
    }

    private void OnDisable()
    {
        _slingshotControls.Main.Disable();
    }
    
    private void OnAimPerformed(InputAction.CallbackContext ctx)
    {
        var screenPosition = ctx.ReadValue<Vector2>();
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(screenPosition), maxClickCheckDistance,
            clickCheckLayerMask);
        if (!rayHit.collider) return;
        
        _isAiming = true;
        _activeProjectile = _projectileSpawner.Spawn(new Vector3(rayHit.point.x, rayHit.point.y, 0));
        var aimingAction = _slingshotControls.Main.Aiming;
        aimingAction.performed += OnAiming;
    }

    private void OnAimCanceled(InputAction.CallbackContext ctx)
    {
        if (!_isAiming) return;

        var aimingAction = _slingshotControls.Main.Aiming;
        aimingAction.performed -= OnAiming;
        _activeProjectile.FireProjectile(transform.position);
        _isAiming = false;
    }

    private void OnAiming(InputAction.CallbackContext ctx)
    {
        var screenPosition = ctx.ReadValue<Vector2>();
        var worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        _activeProjectile.transform.position = worldPosition;
    }
}
