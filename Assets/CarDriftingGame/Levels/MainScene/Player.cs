using System;
using System.Collections.Generic;
using CarDriftingGame.System.Input;
using CarDriftingGame.UI.MainScene;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CarDriftingGame.Levels.MainScene
{
    #region CustomClasses

    public enum Axel
    {
        Front,
        Rear
    }
    public enum Side
    {
        Front,
        Back
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
        
    [Serializable]
    public struct Light
    {
        public GameObject lightObj;
        public Side side;
    }    
    
    #endregion

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class Player : MonoBehaviour
    {
        #region SerializeField

        [Header("CAMERA SETTINGS")] 
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _moveSmoothness;
        [SerializeField] private float _rotSmoothness;

        [SerializeField] private Vector3 _moveOffset;
        [SerializeField] private Vector3 _rotOffset;

        [SerializeField] private Transform _target;
    
        [Header("MOVEMENT")]
        [SerializeField] private float _maxAcceleration = 130f;
        [SerializeField] private float _brakeAcceleration = 3000f;

        [SerializeField] private float _turnSensivity = 1f;
        [SerializeField] private float _maxSteerAngle = 40f;
    
        [Header("SOUNDS")]
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;
    
        [field: Header("CAR PARTS")]
        [field: SerializeField] public List<Wheel> _wheels { get; private set; }
        [FormerlySerializedAs("_Lights")] public List<Light> _lights;
    
        [Header("PHYSICS")]
        [SerializeField] private Vector3 _centerOfMass;
    
        #endregion

        #region Private

        private UIController _uiController;
        [Inject]
        private void Construct(UIController uiController)
        {
            _uiController = uiController;
        }
        
        private bool _isInitialized;
    
        private PlayerMovement _playerMovement;
        private PlayerCamera _playerCamera;
        private SkidTrail _skidTrail;
        private EngineSounds _engineSounds;
        private PlayerLights _playerLights;

        private Rigidbody _rigidbody;
        private AudioSource _audioSource;

        #endregion

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
        
        }

        public void Initialize(InputManager inputManager)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _audioSource = GetComponent<AudioSource>();

            GameObject camera = Instantiate(_cameraTransform.gameObject);
            _cameraTransform = camera.transform;
            
            
            _playerMovement = new PlayerMovement(inputManager, _rigidbody, _wheels);
            _playerCamera = new PlayerCamera(_cameraTransform, _target);
            _skidTrail = new SkidTrail(_wheels, _playerMovement);
            
            
            _engineSounds = new EngineSounds(_rigidbody, _audioSource);
            _engineSounds.SetMinSpeed(_minSpeed);
            _engineSounds.SetMaxSpeed(_maxSpeed);
            _engineSounds.SetMinPitch(_minPitch);
            _engineSounds.SetMaxPitch(_maxPitch);
            
            _playerLights = new PlayerLights(_playerMovement, _lights);
            
            SetCenterOfMass(_centerOfMass);
            
            _isInitialized = true;
        }

        private void FixedUpdate()
        {
            if (_isInitialized)
            {
                _playerMovement.HandleMovement();
                _playerMovement.Steer();
            
                _playerMovement.AnimateWheels();
                _playerMovement.IsDrifting();
            
                _playerCamera.HandleMovement();
                _playerCamera.HandleRotation();
            
                _skidTrail.WheelEffects();
                
                _engineSounds.ControllSounds();
                
                _playerLights.HandleBackLight();
            }
        }

        public void SetCenterOfMass(Vector3 centerOfMass)
        {
            _rigidbody.centerOfMass = centerOfMass;
        }
    }
}