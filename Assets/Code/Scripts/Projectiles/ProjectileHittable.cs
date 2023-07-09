using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Framework.Health;
using UnityEngine;
using Zenject;

public class ProjectileHittable : MonoBehaviour, IDamageTaker
{

    private IDamageTaker _damageTaker;
    
    [Inject]
    private void Construct(IDamageTaker damageTaker)
    {
        _damageTaker = damageTaker;
    }
    
    public void TakeDamage(IDamageApplier damageApplier)
    {
        _damageTaker.TakeDamage(damageApplier);
    }
}
