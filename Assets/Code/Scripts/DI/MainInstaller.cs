using Code.Scripts.DI;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject pathPointPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle();
        Container.Bind<PathPoint>().WithId(InjectionId.DummyPathPointId).FromComponentInNewPrefab(pathPointPrefab)
            .AsSingle();
    }
}