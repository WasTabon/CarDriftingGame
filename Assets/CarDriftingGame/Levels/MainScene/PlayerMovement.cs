using System.Collections.Generic;
using CarDriftingGame.System.Input;
using UnityEngine;

namespace CarDriftingGame.Levels.MainScene
{
    public class PlayerMovement
    {
        private const int MultiplierMove = 600;
        private const float Interpolation = 0.6f;
        
        private float _maxAcceleration = 130f;
        private float _brakeAcceleration = 3000f;

        private float _turnSensivity = 1.0f;
        private float _maxSteerAngle = 40.0f;

        private Vector3 _centerOfMass = new Vector3(0, -0.25f, -1f);

        private List<Wheel> _wheels;

        private InputManager _inputManager;
        private Rigidbody _rigidbody;

        private bool _isBraking;

        private float _verticalInput;
        private float _horizontalInput;

        private float _sidewaysSlipThreshold = 0.5f;
        
        public PlayerMovement(InputManager inputManager, Rigidbody rigidbody, List<Wheel> wheels)
        {
            _inputManager = inputManager;
            _rigidbody = rigidbody;
            _wheels = wheels;
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
                bool isSidewaySlip = Mathf.Abs(hit.sidewaysSlip) > _sidewaysSlipThreshold;

                if (isGroundHit && isSidewaySlip)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
