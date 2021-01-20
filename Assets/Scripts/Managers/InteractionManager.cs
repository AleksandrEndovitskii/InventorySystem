using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Components.InteractionComponents;
using Components.TargetingComponents;
using UnityEngine;

namespace Managers
{
    public class InteractionManager : MonoBehaviour
    {
        public Action<IInteractable> Interacted = delegate {  };
        public Action<IInteractable> SelectedInteractableChanged = delegate {  };

        [SerializeField]
        private List<KeyCodeDuration> _keyCodeTimeSpans = new List<KeyCodeDuration>();

        [Serializable]
        public class KeyCodeDuration
        {
            public KeyCode KeyCode;
            public float TimeSpan;
        }

        public IInteractable SelectedInteractable
        {
            get
            {
                return _selectedInteractable;
            }
            set
            {
                if (_selectedInteractable == value)
                {
                    return;
                }

                _selectedInteractable = value;

                SelectedInteractableChanged.Invoke(_selectedInteractable);
            }
        }

        private IInteractable _selectedInteractable;
        private Coroutine _interactionAfterDelayCoroutine;

        public void Initialize()
        {
            Subscribe();
        }
        public void UnInitialize()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            GameManager.Instance.TargetingManager.TargetedObjectChanged += TargetingManagerOnTargetedObjectChanged;

            GameManager.Instance.InputManager.KeyPressed += InputManagerOnKeyPressed;
            GameManager.Instance.InputManager.KeyUnPressed += InputManagerOnKeyUnPressed;
        }

        private void UnSubscribe()
        {
            if (GameManager.Instance.TargetingManager != null &&
                GameManager.Instance.TargetingManager.TargetedObjectChanged != null)
            {
                GameManager.Instance.TargetingManager.TargetedObjectChanged -= TargetingManagerOnTargetedObjectChanged;
            }

            if (GameManager.Instance.InputManager != null &&
                GameManager.Instance.InputManager.KeyPressed != null)
            {
                GameManager.Instance.InputManager.KeyPressed -= InputManagerOnKeyPressed;
                GameManager.Instance.InputManager.KeyUnPressed -= InputManagerOnKeyUnPressed;
            }
        }

        private void InputManagerOnKeyPressed(KeyCode keyCode)
        {
            var keyCodeDuration = _keyCodeTimeSpans.FirstOrDefault(x => x.KeyCode == keyCode);
            if (keyCodeDuration == null)
            {
                return;
            }

            if (SelectedInteractable == null)
            {
                return;
            }

            _interactionAfterDelayCoroutine = StartCoroutine(InvokeActionAfterDelay(() =>
                {
                    Interacted.Invoke(SelectedInteractable);
                },
                keyCodeDuration.TimeSpan));
        }
        private void InputManagerOnKeyUnPressed(KeyCode keyCode)
        {
            var keyCodeDuration = _keyCodeTimeSpans.FirstOrDefault(x => x.KeyCode == keyCode);
            if (keyCodeDuration == null)
            {
                return;
            }

            if (SelectedInteractable == null)
            {
                return;
            }

            if (_interactionAfterDelayCoroutine != null)
            {
                StopCoroutine(_interactionAfterDelayCoroutine);
            }
        }

        private void TargetingManagerOnTargetedObjectChanged(TargetableComponent targetableComponent)
        {
            SelectedInteractable = GameManager.Instance.TargetingManager.TargetedObject?.gameObject
                .GetComponent<IInteractable>();
        }

        private IEnumerator InvokeActionAfterDelay(Action action, float delaySeconds = 0f)
        {
            yield return new WaitForSeconds(delaySeconds);

            action?.Invoke();

            yield return null;
        }
    }
}
