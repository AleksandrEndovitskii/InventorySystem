using System;
using Components.TargetingComponents;
using UnityEngine;

namespace Managers
{
    public class TargetingManager : MonoBehaviour
    {
        public Action<TargetableComponent> TargetedObjectChanged = delegate {  };

        [SerializeField]
        private float _selectionRange = 1000f;

        public TargetableComponent TargetedObject
        {
            get
            {
                return _targetedObject;
            }
            set
            {
                if (_targetedObject == value)
                {
                    return;
                }

                var previouslySelectedObjectName = "None";
                if (_targetedObject != null &&
                    _targetedObject.gameObject != null)
                {
                    previouslySelectedObjectName = _targetedObject.gameObject.name;
                }

                var currentlySelectedObjectName = "None";
                if (value != null &&
                    value.gameObject != null)
                {
                    currentlySelectedObjectName = value.gameObject.name;
                }

                Debug.Log(
                    $"Targeted object changed from {previouslySelectedObjectName} to {currentlySelectedObjectName}");

                _targetedObject = value;

                TargetedObjectChanged.Invoke(_targetedObject);
            }
        }

        private TargetableComponent _targetedObject;

        private void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // dont hit anything - nothing to do here
            if (!Physics.Raycast(ray, out var hit))
            {
                Debug.DrawRay(ray.origin, ray.direction * _selectionRange, Color.red);
                TargetedObject = null;

                return;
            }

            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green);
            TargetedObject = hit.transform.gameObject.GetComponent<TargetableComponent>();
        }
    }
}