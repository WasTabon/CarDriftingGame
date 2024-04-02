using System;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

namespace CarDriftingGame.UI.MainScene
{
    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
    
    public class UIController : IUpdatable
    {
        public event Action<Vector2> InputPressed; 

        private Vector2 _inputDir;
        private CarInput _carInput;
        private Direction _currentDirection = Direction.None;

        public void Initialize(CarInput carInput, Button gasButton, Button brakeButton, Button rightButton, Button leftButton)
        {
            _carInput = carInput;
            
            ConnectButtonToKeyCode(gasButton, "<Keyboard>/w");
            ConnectButtonToKeyCode(brakeButton, "<Keyboard>/s");
            ConnectButtonToKeyCode(rightButton, "<Keyboard>/d");
            ConnectButtonToKeyCode(leftButton, "<Keyboard>/a");
        }

        public void Update()
        {
            GetInvokeInput();
        }

        private void GetInvokeInput()
        {
            _inputDir = _carInput.UI.Move.ReadValue<Vector2>();
            InputPressed?.Invoke(_inputDir);
        }

        private void ConnectButtonToKeyCode(Button button, string keyCode)
        {
            button.gameObject.AddComponent<OnScreenButton>().controlPath = keyCode;
        }
    }
}
