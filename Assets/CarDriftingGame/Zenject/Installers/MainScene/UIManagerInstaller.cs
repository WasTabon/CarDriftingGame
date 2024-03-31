using CarDriftingGame.UI.MainScene;
using Zenject;

public class UIManagerInstaller : MonoInstaller
{
    //[SerializeField] private UIController _uiController;
    public override void InstallBindings()
    {
        //Container.Bind<UIController>().FromInstance(_uiController)
            //.AsSingle()
            //.NonLazy();
        
        Container.Bind<UIController>().FromNew().AsSingle().NonLazy();
    }
}