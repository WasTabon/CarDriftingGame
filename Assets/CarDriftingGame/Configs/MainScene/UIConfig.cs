using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.OnScreen;

namespace CarDriftingGame.Configs.MainScene
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Configs / New UIConfig")]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private string _gasButtonTag;
        [SerializeField] private string _brakeButtonTag;
        [SerializeField] private string _turnRightButtonTag;
        [SerializeField] private string _turnLeftButtonTag;
        [SerializeField] private string _lightButtonTag;

        [SerializeField] private OnScreenControl _key;
        
        public string GasButtonTag => _gasButtonTag;
        public string BrakeButtonTag => _brakeButtonTag;
        public string TurnRightButtonTag => _turnRightButtonTag;
        public string TurnLeftButtonTag => _turnLeftButtonTag;
        public string LightButtonTag => _lightButtonTag;
    }
}
