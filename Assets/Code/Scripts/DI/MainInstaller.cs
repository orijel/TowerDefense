using Code.Scripts.DI;
using Code.Scripts.Framework.Health;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject pathPointPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawner;
    [SerializeField] private GameObject projectilePrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle();
        Container.Bind<PathPoint>().WithId(InjectionId.DummyPathPointId).FromComponentInNewPrefab(pathPointPrefab)
            .AsSingle();
        Container.Bind<IDamageTaker>().To<Enemy>().FromComponentInParents();

        //TODO: bind all enemy spawners
        Container.Bind<EnemySpawner>().FromComponentOn(enemySpawner).AsSingle();

        Container.BindMemoryPool<Enemy, Enemy.Factory>().WithInitialSize(5).FromComponentInNewPrefab(enemyPrefab)
            .UnderTransformGroup("EnemyPool");
        Container.BindMemoryPool<Projectile, Projectile.Factory>().WithInitialSize(3).FromComponentInNewPrefab(projectilePrefab)
            .UnderTransformGroup("ProjectilePool");

    }
}