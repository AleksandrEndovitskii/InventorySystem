using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Canvas))]
    public class MainCameraCanvasWorldCameraSetterComponent : MonoBehaviour
    {
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = this.gameObject.GetComponent<Canvas>();
        }
        private void Start()
        {
            _canvas.worldCamera = Camera.main;
        }
    }
}
