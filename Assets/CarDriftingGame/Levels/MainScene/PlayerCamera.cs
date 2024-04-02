using UnityEngine;

namespace CarDriftingGame.Levels.MainScene
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
    
        [SerializeField] private float _moveSmoothness;
        [SerializeField] private float _rotSmoothness;

        [SerializeField] private Vector3 _moveOffset;
        [SerializeField] private Vector3 _rotOffset;

        [SerializeField] private Transform _target;

        private void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        public void HandleMovement()
        {
            var targetPos = _target.TransformPoint(_moveOffset);

            _camera.position = Vector3.Lerp(_camera.position, targetPos, _moveSmoothness * Time.deltaTime);
        }

        public void HandleRotation()
        {
            Vector3 direction = _target.position - _camera.position;
            Quaternion rotation = Quaternion.LookRotation(direction + _rotOffset, Vector3.up);

            _camera.rotation = Quaternion.Lerp(_camera.rotation, rotation, _rotSmoothness * Time.deltaTime);
        }
    }
}
