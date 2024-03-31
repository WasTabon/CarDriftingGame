using System;
using CarDriftingGame.System.Input;
using UnityEngine;
using Zenject;

public class GameTester : MonoBehaviour
{
    [Inject] private InputManager _inputManager;

    private void Update()
    {
        //Debug.Log($"Vertical: {_inputManager.Verical}");
        //Debug.Log($"Horizontal: {_inputManager.Horizontal}");
    }
}
