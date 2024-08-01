using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private BaseInteractor _interactor;
    [SerializeField] private Base _basePrefab;

    public override void InstallBindings()
    {
        Container.Bind<BaseInteractor>().FromInstance(_interactor).AsSingle();
        Container.BindFactory<Base, Base.Factory>().FromComponentInNewPrefab(_basePrefab);
    }
}