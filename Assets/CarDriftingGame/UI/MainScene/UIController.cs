using System;
using UnityEngine;
using UnityEngine.UI;

namespace CarDriftingGame.UI.MainScene
{
    public class UIController : MonoBehaviour
    {
        [Header("All UI")]
        [SerializeField] private Button _gasButton;
        [SerializeField] private Button _brakeButton;
        [SerializeField] private Button _turnRightButton;
        [SerializeField] private Button _turnLeftButton;

        public event Action<bool> GasPressed;
        public event Action<bool> BrakePressed;
        public event Action<bool> TurnRightPressed;
        public event Action<bool> TurnLeftPressed;

        private bool _isGasPressed;
        private bool _isBrakePressed;
        private bool _isTurnRightPressed;
        private bool _isTurnLeftPressed;

        public void Initialize()
        {
            _gasButton.onClick.AddListener(OnGasClicked);
            _brakeButton.onClick.AddListener(OnBrakeClicked);
            _turnRightButton.onClick.AddListener(OnTurnRightClicked);
            _turnLeftButton.onClick.AddListener(OnTurnLeftClicked);
        }

        private void OnButtonClicked(ref bool state, Action<bool> action)
        {
            state = !state;
            action?.Invoke(state);
        }
        
        private void OnGasClicked()
        {
            OnButtonClicked(ref _isGasPressed, GasPressed);
        }
        private void OnBrakeClicked()
        {
            OnButtonClicked(ref _isBrakePressed, BrakePressed);
        }
        private void OnTurnRightClicked()
        {
            OnButtonClicked(ref _isTurnRightPressed, TurnRightPressed);
        }
        private void OnTurnLeftClicked()
        {
            OnButtonClicked(ref _isTurnLeftPressed, TurnLeftPressed);
        }
    }
}