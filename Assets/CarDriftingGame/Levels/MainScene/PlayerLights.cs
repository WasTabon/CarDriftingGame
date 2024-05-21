using System.Collections.Generic;
using CarDriftingGame.UI.MainScene;
using Zenject;

namespace CarDriftingGame.Levels.MainScene
{
    public class PlayerLights
    {
        private List<Light> _lights;
        private PlayerMovement _playerMovement;
        [Inject] private UIController _uiController;

        private bool _isBackLightOn;

        public PlayerLights(PlayerMovement playerMovement, List<Light> lights)
        {
            _playerMovement = playerMovement;
            _lights = lights;
            
            //_uiController.LightPressed += HandeFrontLight;
        }

        private void HandeFrontLight()
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
