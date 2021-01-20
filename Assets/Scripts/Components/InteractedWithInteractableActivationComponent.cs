using Components.InteractionComponents;
using Managers;
using UnityEngine;

namespace Components
{
    public class InteractedWithInteractableActivationComponent : MonoBehaviour
    {
        [SerializeField]
        private InteractableComponent _interactableComponent;

        private InteractionManager _interactionManager;

        private void Awake()
        {
            _interactionManager = FindObjectOfType<InteractionManager>();
        }
        private void Start()
        {
            _interactionManager.Interacted += OnInteracted;
            OnInteracted(_interactionManager.SelectedInteractable);
        }
        private void OnDestroy()
        {
            _interactionManager.Interacted -= OnInteracted;
        }

        private void OnInteracted(IInteractable interactable)
        {
            if (interactable == null ||
                interactable != _interactableComponent)
            {
                this.gameObject.SetActive(false);

                return;
            }

            this.gameObject.SetActive(true);
        }
    }
}
