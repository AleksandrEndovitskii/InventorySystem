using System;
using System.Collections.Generic;
using System.Linq;
using Models.InventoryItems;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(CollisionDetectionComponent))]
    public class ChildSetterComponent : MonoBehaviour
    {
        public Action<InventoryItemModel> InventoryItemAttached = delegate { };
        public Action<InventoryItemModel> InventoryItemDetached = delegate { };

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
            var inventoryItemModel = collision.gameObject.GetComponent<InventoryItemModel>();
            if (inventoryItemModel == null)
            {
                return;
            }

            var typeTransform = _typeTransforms.FirstOrDefault(x=>x.Type == inventoryItemModel.Type);
            if (typeTransform == null)
            {
                return;
            }

            AttachInventoryItemToSlot(inventoryItemModel, typeTransform.Transform);
        }
        private void OnCollisionExit(Collision obj)
        {

        }

        private void AttachInventoryItemToSlot(InventoryItemModel inventoryItemModel, Transform container)
        {
            inventoryItemModel.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            var collider = inventoryItemModel.gameObject.GetComponent<Collider>();
            Destroy(collider);
            var mouseDraggingGameObjectComponent = inventoryItemModel.gameObject.GetComponent<MouseDraggingGameObjectComponent>();
            Destroy(mouseDraggingGameObjectComponent);
            inventoryItemModel.gameObject.transform.parent = container;
            inventoryItemModel.gameObject.transform.localPosition = Vector3.zero;

            InventoryItemAttached.Invoke(inventoryItemModel);
        }
    }
}
