using Components.TargetingComponents;
using Managers;
using Models;
using UnityEngine;

namespace Components
{
    public class ActivationComponent : MonoBehaviour
    {
        [SerializeField]
        private TargetableComponent _targetableComponent;

        private TargetingManager _targetingManager;

        private void Awake()
        {
            _targetingManager = FindObjectOfType<TargetingManager>();
        }
        private void Start()
        {
            _targetingManager.TargetedObjectChanged += OnTargetedObjectChanged;
            OnTargetedObjectChanged(_targetingManager.TargetedObject);
        }
        private void OnDestroy()
        {
            _targetingManager.TargetedObjectChanged -= OnTargetedObjectChanged;
        }

        private void OnTargetedObjectChanged(TargetableComponent targetableComponent)
        {
            if (targetableComponent == null ||
                targetableComponent != _targetableComponent)
            {
                this.gameObject.SetActive(false);

                return;
            }

            this.gameObject.SetActive(true);
        }
    }
}
