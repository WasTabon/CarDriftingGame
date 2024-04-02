using CarDriftingGame.Configs.MainScene;
using CarDriftingGame.Levels.MainScene;
using CarDriftingGame.System.Input;
using CarDriftingGame.UI.MainScene;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CarDriftingGame.System.EntryPoint.MainScene
{
    public class MainSceneEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIConfig _uiConfig;

        private InputManager _inputManager;
        private UIController _uiController;
        private Updater _updater;

        private CarInput _carInput;
        
        private Button _gasButton;
        private Button _brakeButton;
        private Button _leftButton;
        private Button _rightButton;
        private Button _lightButton;

        [Inject]
        private void Construct(InputManager inputManager, UIController uiController)
        {
            _inputManager = inputManager;
            _uiController = uiController;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateInputSystem();
            
            InitializeUpdater();
            
            _updater.RegisterUpdatable(_uiController);
            
            GetButtons();

            _uiController.Initialize(_carInput, _gasButton, _brakeButton, _rightButton, _leftButton, _lightButton);
            _inputManager.Initialize(_uiController);
        }

        private void CreateInputSystem()
        {
            _carInput = new CarInput();
            _carInput.Enable();
        }
        private void GetButtons()
        {
            _gasButton = GameObject.FindWithTag(_uiConfig.GasButtonTag).GetComponent<Button>();
            _brakeButton = GameObject.FindWithTag(_uiConfig.BrakeButtonTag).GetComponent<Button>();
            _rightButton = GameObject.FindWithTag(_uiConfig.TurnRightButtonTag).GetComponent<Button>();
            _leftButton = GameObject.FindWithTag(_uiConfig.TurnLeftButtonTag).GetComponent<Button>(); 
            _lightButton = GameObject.FindWithTag(_uiConfig.LightButtonTag).GetComponent<Button>(); 
        }

        private void InitializeUpdater()
        {
            GameObject created = new GameObject("Updater");
            Updater updater = created.AddComponent<Updater>();
            updater.Initialize();
            
            _updater = updater;
        }
        
    }
}
