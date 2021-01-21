using System.Collections.Generic;
using Models;
using Models.InventoryItems;
using UnityEngine;
using View.InventoryItems;

namespace Containers
{
    public class InventoryItemIconsVerticalContainer : MonoBehaviour
    {
        [SerializeField]
        private InventoryItemView _inventoryItemViewPrefab;

        private List<InventoryItemView> _inventoryItemViewInstances = new List<InventoryItemView>();

        private void Start()
        {
            Initialize();
        }
        private void OnDestroy()
        {
            UnInitialize();
        }

        public void Initialize()
        {
            var backpackModel = FindObjectOfType<BackpackModel>();
            var inventoryItemModels = backpackModel.InventoryItemModels;

            _inventoryItemViewInstances = InstantiateInventoryItems(inventoryItemModels);
        }
        public void UnInitialize()
        {
            foreach (var inventoryItemViewInstance in _inventoryItemViewInstances)
            {
                if (inventoryItemViewInstance == null)
                {
                    continue;
                }

                Destroy(inventoryItemViewInstance.gameObject);
            }
            _inventoryItemViewInstances.Clear();
        }

        private List<InventoryItemView> InstantiateInventoryItems(List<InventoryItemModel> inventoryItemModels)
        {
            var inventoryItemViewInstances = new List<InventoryItemView>();

            foreach (var inventoryItemModel in inventoryItemModels)
            {
                var inventoryItemViewInstance = Create(inventoryItemModel);
                inventoryItemViewInstances.Add(inventoryItemViewInstance);
            }

            return inventoryItemViewInstances;
        }

        public InventoryItemView Create(InventoryItemModel inventoryItemModel)
        {
            var inventoryItemViewInstance = Instantiate(_inventoryItemViewPrefab);
            inventoryItemViewInstance.InventoryItemModel = inventoryItemModel;
            return inventoryItemViewInstance;
        }
    }
}
