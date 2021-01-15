using UnityEngine;

namespace Components
{
    public class MouseDraggingGameObjectComponent : MonoBehaviour
    {
        private Camera _mainCamera;

        private Vector3 _offsetBetweenObjectWorldPositionAndMouseWorldPosition;
        private float _gameObjectScreenPositionZ;

        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void OnMouseDown()
        {
            var gameObjectWorldPosition = this.gameObject.transform.position;
            _gameObjectScreenPositionZ = _mainCamera.WorldToScreenPoint(gameObjectWorldPosition).z;

            var mouseWorldPosition = GetMouseWorldPosition();
            _offsetBetweenObjectWorldPositionAndMouseWorldPosition = gameObjectWorldPosition - mouseWorldPosition;
        }

        private void OnMouseDrag()
        {
            var mouseWorldPosition = GetMouseWorldPosition();
            this.gameObject.transform.position = mouseWorldPosition + _offsetBetweenObjectWorldPositionAndMouseWorldPosition;
        }

        private Vector3 GetMouseWorldPosition()
        {
            var mouseScreenPosition = Input.mousePosition;
            mouseScreenPosition.z = _gameObjectScreenPositionZ;

            var mouseWorldPosition = _mainCamera.ScreenToWorldPoint(mouseScreenPosition);
            return mouseWorldPosition;
        }
    }
}
