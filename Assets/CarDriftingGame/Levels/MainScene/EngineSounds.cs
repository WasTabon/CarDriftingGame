using System;
using UnityEngine;

namespace CarDriftingGame.Levels.MainScene
{
    public class EngineSounds : MonoBehaviour
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;

        private float _currentSpeed;
        private float _pitchFromCar;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ControllSounds();
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
