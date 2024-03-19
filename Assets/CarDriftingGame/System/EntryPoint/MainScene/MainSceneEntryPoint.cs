using CarDriftingGame.System.Input;
using CarDriftingGame.UI.MainScene;
using UnityEngine;
using Zenject;

public class MainSceneEntryPoint : MonoBehaviour
{
    private InputManager _inputManager;
    private UIController _uiController;

    [Inject]
    private void Construct(InputManager inputManager, UIController uiController)
    {
        _inputManager = inputManager;
        _uiController = uiController;
    }

    private void Start()
    {
        _uiController.Initialize();
        
        
    }
}
