using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private LevelManager levelManager;
    
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(levelManager).AsSingle();
    }
}