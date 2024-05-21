using UnityEngine;

namespace CarDriftingGame.Levels.MainScene
{
    public class EngineSounds
    {
        private float _minSpeed;
        private float _maxSpeed;
        
        private float _minPitch;
        private float _maxPitch;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;

        private float _currentSpeed;
        private float _pitchFromCar;

        public void SetMinSpeed(float amount) => _minSpeed = amount;
        public void SetMaxSpeed(float amount) => _maxSpeed = amount;
        public void SetMinPitch(float amount) => _minPitch = amount;
        public void SetMaxPitch(float amount) => _maxPitch = amount;
        
        public EngineSounds(Rigidbody rigidbody, AudioSource audioSource)
        {
            _rigidbody = rigidbody;
            _audioSource = audioSource;
        }
        
        public void ControllSounds()
        {
            _currentSpeed = _rigidbody.velocity.magnitude;
            _pitchFromCar = _rigidbody.velocity.magnitude / 50f;
            
            if (_currentSpeed < _minSpeed) 
                _audioSource.pitch = _minPitch;

            if (_currentSpeed > _minSpeed && _currentSpeed < _maxSpeed)
                _audioSource.pitch = _minPitch + _pitchFromCar;

            if (_currentSpeed > _maxSpeed)
                _audioSource.pitch = _minPitch;
        }
    }
}
