using CarDriftingGame.UI.MainScene;
using UnityEngine;
using Zenject;

public class UIManagerInstaller : MonoInstaller
{
    [SerializeField] private UIController _uiController;
    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromInstance(_uiController)
            .AsSingle()
            .NonLazy();
    }
}