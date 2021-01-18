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

            Destroy(collision.gameObject);
            // collision.gameObject.transform.parent = typeTransform.Transform;
            // collision.gameObject.transform.localPosition = Vector3.zero;
        }
        private void OnCollisionExit(Collision obj)
        {

        }
    }
}
