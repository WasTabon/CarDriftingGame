using UnityEngine;

namespace CarDriftingGame.System.Input
{
    public class InputManager
    {
        public Vector2 Horizontal { get; private set; }

        public Vector2 Vertical { get; private set; }
        
        public bool Gasing { get; private set; }

        public bool Braking { get; private set; }
    }
}
