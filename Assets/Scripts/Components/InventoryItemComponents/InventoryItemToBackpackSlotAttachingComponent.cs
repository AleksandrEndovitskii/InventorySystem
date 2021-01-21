using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Models;
using Models.InventoryItems;
using UnityEngine;
using View.InventoryItems;

namespace Components.InventoryItemComponents
{
    [RequireComponent(typeof(BackpackModel))]
    [RequireComponent(typeof(InventoryItemsOnCollisionEnterBakcpackAddingComponent))]
    public class InventoryItemToBackpackSlotAttachingComponent : MonoBehaviour
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

        private BackpackModel _backpackModel;
        private GameObjectsManager _gameObjectsManager;

        private void Awake()
        {
            _backpackModel = this.gameObject.GetComponent<BackpackModel>();

            _gameObjectsManager = FindObjectOfType<GameObjectsManager>();
        }
        private void Start()
        {
            _backpackModel.InventoryItemModelAdded += OnInventoryItemModelAdded;
            _backpackModel.InventoryItemModelRemoved += OnInventoryItemModelRemoved;
            foreach (var inventoryItemModel in _backpackModel.InventoryItemModels)
            {
                OnInventoryItemModelAdded(inventoryItemModel);
            }
        }
        private void OnDestroy()
        {
            _backpackModel.InventoryItemModelAdded -= OnInventoryItemModelAdded;
            _backpackModel.InventoryItemModelRemoved -= OnInventoryItemModelRemoved;
        }

        private void OnInventoryItemModelAdded(InventoryItemModel inventoryItemModel)
        {
            var inventoryItemView = _gameObjectsManager.Create(inventoryItemModel);

            var typeTransform = _typeTransforms.FirstOrDefault(x =>
                x.Type == inventoryItemView.InventoryItemModel.Type);
            if (typeTransform == null)
            {
                return;
            }

            AttachInventoryItemToSlot(inventoryItemView, typeTransform.Transform);
        }
        private void OnInventoryItemModelRemoved(InventoryItemModel inventoryItemModel)
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
