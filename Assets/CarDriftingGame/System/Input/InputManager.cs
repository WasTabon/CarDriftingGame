using UnityEngine;

namespace CarDriftingGame.System.Input
{
    public class InputManager
    {
        public Vector2 Horizontal
        {
            get => _horizontal;
        }
    
        public Vector2 Vertical
        {
            get => _vertical;
        }
    
        private Vector2 _horizontal;
        private Vector2 _vertical;
    }
}
