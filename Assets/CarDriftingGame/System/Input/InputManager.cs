using CarDriftingGame.UI.MainScene;
using UnityEngine;

namespace CarDriftingGame.System.Input
{
    public class InputManager
    {
        public float Vertical { get; private set; }
        public float Horizontal { get; private set; }
        
        public void Initialize(UIController uiController)
        {
            uiController.InputPressed += SetInput;
        }

        private void SetInput(Vector2 value)
        {
            Vertical = value.y;
            Horizontal = value.x;
        }
    }
}
