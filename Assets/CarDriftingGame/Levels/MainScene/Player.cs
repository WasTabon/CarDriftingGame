using System;
using System.Collections.Generic;
using CarDriftingGame.System.Input;
using UnityEngine;

#region CustomClasses

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

#endregion

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    #region SerializeField
    
    [Header("MOVEMENT")]
    [SerializeField] private float _maxAcceleration = 130f;
    [SerializeField] private float _brakeAcceleration = 3000f;

    [SerializeField] private float _turnSensivity = 1f;
    [SerializeField] private float _maxSteerAngle = 40f;
    
    [field: Header("WHEELS")]
    [field: SerializeField] public List<Wheel> _wheels { get; private set; }
    
    [Header("PHYSICS")]
    [SerializeField] private Vector3 _centerOfMass;
    
    #endregion

    #region Private

    private InputManager _inputManager;
    private Rigidbody _rigidbody;

    #endregion
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        SetCenterOfMass(_centerOfMass);
    }

    public void Initialize(Vector3 spawnPosition, InputManager inputManager)
    {
        
    }

    private void SetCenterOfMass(Vector3 centerOfMass)
    {
        _rigidbody.centerOfMass = centerOfMass;
    }
}
