using System;
using System.Collections.Generic;
using CarDriftingGame.UI.MainScene;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CarDriftingGame.Levels.MainScene
{
    public class PlayerLights : MonoBehaviour
    {
        public enum Side
        {
            Front,
            Back
        }
        
        [Serializable]
        public struct Light
        {
            public GameObject lightObj;
            public Side side;
        }

        [FormerlySerializedAs("_Lights")] public List<Light> _lights;
        private PlayerMovement _playerMovement;
        private UIController _uiController;

        private bool _isBackLightOn;

        [Inject]
        private void Construct(UIController uiController)
        {
            _uiController = uiController;
        }
        
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();

            _uiController.LightPressed += HandeFrontLight;
        }

        private void Update()
        {
            HandleBackLight();
        }

        public void HandeFrontLight()
        {
            foreach (Light light in _lights)
            {
                if (light.side == Side.Front)
                {
                    light.lightObj.SetActive(!light.lightObj.activeSelf);
                }
            }
        }
        
        public void HandleBackLight()
        {
            if (_playerMovement.IsDrifting() && !_isBackLightOn)
            {
                foreach (Light light in _lights)
                {
                    if (light.side == Side.Back)
                        light.lightObj.SetActive(true);
                }

                _isBackLightOn = true;
            }
            else if (!_playerMovement.IsDrifting() && _isBackLightOn)
            {
                foreach (Light light in _lights)
                {
                    if (light.side == Side.Back)
                        light.lightObj.SetActive(false);
                }
                
                _isBackLightOn = false;
            }
        }
    }
}
