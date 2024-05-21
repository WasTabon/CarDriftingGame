using UnityEngine;

namespace CarDriftingGame.Levels.MainScene
{
    public class PlayerCamera
    {
        private Transform _camera;
    
        private float _moveSmoothness = 5;
        private float _rotSmoothness = 7;

        private Vector3 _moveOffset;
        private Vector3 _rotOffset;

        private Transform _target;

        public PlayerCamera(Transform camera, Transform target)
        {
            _moveOffset = new Vector3(0, 6, -7);
            
            _camera = camera;
            _target = target;
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
