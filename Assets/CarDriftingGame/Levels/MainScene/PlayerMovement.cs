using System;
using System.Collections.Generic;
using CarDriftingGame.System.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CarDriftingGame.Levels.MainScene
{
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }
    
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private const int Multiplier = 600;
        private const float Interpolation = 0.6f;
        
        [SerializeField] private float _maxAcceleration = 30f;
        [SerializeField] private float _brakeAcceleration = 50f;
        
        [SerializeField] private float _turnSensivity = 1.0f;
        [SerializeField] private float _maxSteerAngle = 30.0f;
        
        [SerializeField] private List<Wheel> _wheels;
        
        private InputManager _inputManager;
        private Rigidbody _rigidbody;

        private Vector3 _centerOfMass;
        
        private float _verticalInput;
        private float _horizontalInput;

        [Inject]
        private void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.centerOfMass = _centerOfMass;
        }

        private void Update()
        {
            Move();
            Steer();
            AnimateWheels();
        }

        public void Move()
        {
            _verticalInput = _inputManager.Vertical;
            
            foreach (var wheel in _wheels)
            {
                wheel.wheelCollider.motorTorque = _verticalInput * _maxAcceleration * Multiplier * Time.deltaTime;
            }
        }

        public void Steer()
        {
            _horizontalInput = _inputManager.Horizontal;
            
            foreach (var wheel in _wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    var _steerAngle = _horizontalInput * _turnSensivity * _maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, Interpolation);
                }
            }
        }

        public void AnimateWheels()
        {
            foreach (var wheel in _wheels)
            {
                Quaternion rotation;
                Vector3 position;
                
                wheel.wheelCollider.GetWorldPose(out position, out rotation);
                wheel.wheelModel.transform.position = position;
                wheel.wheelModel.transform.rotation = rotation;
            }
        }
    }
}
