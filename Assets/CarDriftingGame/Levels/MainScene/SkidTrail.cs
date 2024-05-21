using System.Collections.Generic;

namespace CarDriftingGame.Levels.MainScene
{
    public class SkidTrail
    {
        private List<Wheel> _wheels;
        
        private PlayerMovement _playerMovement;

        public SkidTrail(List<Wheel> wheels, PlayerMovement playerMovement)
        {
            _wheels = wheels;
            _playerMovement = playerMovement;
        }
        
        public void WheelEffects()
        {
            foreach (Wheel wheel in _wheels)
            {
                if (wheel.axel == Axel.Rear)
                {
                    wheel.trailRenderer.emitting = _playerMovement.IsDrifting();
                    if (_playerMovement.IsDrifting())
                        wheel.particleSystem.Emit(1);
                    else
                        wheel.particleSystem.Emit(0);
                }
            }
        }
    }
}
