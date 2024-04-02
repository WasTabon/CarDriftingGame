using UnityEngine;

namespace CarDriftingGame.Levels.MainScene
{
    public class SkidTrail : MonoBehaviour
    {
        private PlayerMovement _playerMovement;

        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void FixedUpdate()
        {
            WheelEffects();
        }

        public void WheelEffects()
        {
            foreach (Wheel wheel in _playerMovement._wheels)
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
