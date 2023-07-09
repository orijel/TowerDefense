using Code.Scripts.DI;
using Code.Scripts.Framework.Health;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject pathPointPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemySpawnersRootObj;
    [SerializeField] private GameObject projectilePrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle();
        Container.Bind<PathPoint>().WithId(InjectionId.DummyPathPointId).FromComponentInNewPrefab(pathPointPrefab)
            .AsSingle();
        Container.Bind<IDamageTaker>().To<Enemy>().FromComponentInParents();

        Container.Bind<EnemySpawner>()
            .FromMethodMultiple(_ => enemySpawnersRootObj.GetComponentsInChildren<EnemySpawner>()).AsSingle();

        Container.BindMemoryPool<Enemy, Enemy.Factory>().WithInitialSize(5).FromComponentInNewPrefab(enemyPrefab)
            .UnderTransformGroup("EnemyPool");
        Container.BindMemoryPool<Projectile, Projectile.Factory>().WithInitialSize(3).FromComponentInNewPrefab(projectilePrefab)
            .UnderTransformGroup("ProjectilePool");

    }
}