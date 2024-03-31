using CarDriftingGame.System.Input;
using Zenject;

public class InputManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromNew().AsSingle().NonLazy();
    }
}