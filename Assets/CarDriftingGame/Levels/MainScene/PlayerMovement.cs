using System;
using System.Collections.Generic;
using CarDriftingGame.System.Input;
using UnityEngine;
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
        public TrailRenderer trailRenderer;
        public ParticleSystem particleSystem;
        public Axel axel;
    }

    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private const int MultiplierMove = 600;
        private const float Interpolation = 0.6f;
        
        [SerializeField] private float _maxAcceleration = 30f;
        [SerializeField] private float _brakeAcceleration = 50f;

        [SerializeField] private float _turnSensivity = 1.0f;
        [SerializeField] private float _maxSteerAngle = 30.0f;

        [SerializeField] private Vector3 _centerOfMass;

        [field: SerializeField] public List<Wheel> _wheels { get; private set; }

        private InputManager _inputManager;
        private Rigidbody _rigidbody;

        private bool _isBraking;

        private float _verticalInput;
        private float _horizontalInput;

        public float sidewaysSlipThreshold = 0.5f;
        public float steerAngleThreshold = 30f;

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

        private void FixedUpdate()
        {
            HandleMovement();
            Steer();
            
            AnimateWheels();
            IsDrifting();
        }

        public void HandleMovement()
        {
            _verticalInput = _inputManager.Vertical;

            foreach (var wheel in _wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
                wheel.wheelCollider.motorTorque =
                    _verticalInput * _maxAcceleration * MultiplierMove * Time.fixedDeltaTime;
            }
        }

        public void Steer()
        {
            _horizontalInput = _inputManager.Horizontal;

            foreach (var wheel in _wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    float steerAngle = _horizontalInput * _turnSensivity * _maxSteerAngle;
                    wheel.wheelCollider.steerAngle =
                        Mathf.Lerp(wheel.wheelCollider.steerAngle, steerAngle, Interpolation);
                }
            }
        }

        public void AnimateWheels()
        {
            foreach (var wheel in _wheels)
            {
                wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
                
                wheel.wheelModel.transform.position = position;
                wheel.wheelModel.transform.rotation = rotation;
            }
        }

        public bool IsDrifting()
        {
            foreach (Wheel wheel in _wheels)
            {
                WheelHit hit;

                bool isGroundHit = wheel.wheelCollider.GetGroundHit(out hit);
                bool isSidewaySlip = Mathf.Abs(hit.sidewaysSlip) > sidewaysSlipThreshold;

                if (isGroundHit && isSidewaySlip)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
