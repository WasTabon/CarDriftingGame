using CarDriftingGame.UI.MainScene;
using Zenject;

public class UIManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UIController>().FromNew().AsSingle().NonLazy();
    }
}