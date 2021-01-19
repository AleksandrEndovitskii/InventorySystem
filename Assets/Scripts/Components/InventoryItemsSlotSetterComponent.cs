using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View.InventoryItems;

namespace Components
{
    [RequireComponent(typeof(CollisionDetectionComponent))]
    public class InventoryItemsSlotSetterComponent : MonoBehaviour
    {
        public Action<InventoryItemView> InventoryItemAttached = delegate { };
        public Action<InventoryItemView> InventoryItemDetached = delegate { };

        [SerializeField]
        private List<TypeTransform> _typeTransforms = new List<TypeTransform>();

        [Serializable]
        public class TypeTransform
        {
            public string Type;
            public Transform Transform;
        }

        private CollisionDetectionComponent _collisionDetectionComponent;

        private void Awake()
        {
            _collisionDetectionComponent = this.gameObject.GetComponent<CollisionDetectionComponent>();
        }
        private void Start()
        {
            _collisionDetectionComponent.CollisionEnter += OnCollisionEnter;
            _collisionDetectionComponent.CollisionExit += OnCollisionExit;
        }
        private void OnDestroy()
        {
            _collisionDetectionComponent.CollisionEnter -= OnCollisionEnter;
            _collisionDetectionComponent.CollisionExit -= OnCollisionExit;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var inventoryItemView = collision.gameObject.GetComponent<InventoryItemView>();
            if (inventoryItemView == null)
            {
                return;
            }

            var typeTransform = _typeTransforms.FirstOrDefault(x =>
                x.Type == inventoryItemView.InventoryItemModel.Type);
            if (typeTransform == null)
            {
                return;
            }

            AttachInventoryItemToSlot(inventoryItemView, typeTransform.Transform);
        }
        private void OnCollisionExit(Collision collision)
        {

        }

        private void AttachInventoryItemToSlot(InventoryItemView inventoryItemView, Transform slot)
        {
            // disable gravity
            inventoryItemView.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            // disable collision detection
            var collider = inventoryItemView.gameObject.GetComponent<Collider>();
            Destroy(collider);
            // disable mouse dragging
            var mouseDraggingGameObjectComponent = inventoryItemView.gameObject.GetComponent<MouseDraggingGameObjectComponent>();
            Destroy(mouseDraggingGameObjectComponent);
            // set inventory item to slot
            inventoryItemView.gameObject.transform.parent = slot;
            inventoryItemView.gameObject.transform.localPosition = Vector3.zero;

            InventoryItemAttached.Invoke(inventoryItemView);
        }
    }
}
