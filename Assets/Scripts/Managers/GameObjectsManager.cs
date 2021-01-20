using System.Collections.Generic;
using Models.InventoryItems;
using UnityEngine;
using Utils;
using View.InventoryItems;

namespace Managers
{
    public class GameObjectsManager : MonoBehaviour, IInitilizable, IUnInitializeble
    {
        [SerializeField]
        private InventoryItemView _inventoryItemViewPrefab;
        [SerializeField]
        private Vector3 _initialPosition = new Vector3(26, 1, 10);
        [SerializeField]
        private Vector3 _positionStep = new Vector3(2, 0, 0);

        private List<InventoryItemView> _inventoryItemViewInstances = new List<InventoryItemView>();

        public void Initialize()
        {
            var inventoryItemModels = GetInventoryItemModels();

            _inventoryItemViewInstances = InstantiateInventoryItems(inventoryItemModels);

            SetInventoryItemsPositions(_inventoryItemViewInstances, _initialPosition, _positionStep);
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

        public InventoryItemView Create(InventoryItemModel inventoryItemModel)
        {
            var inventoryItemViewInstance = Instantiate(_inventoryItemViewPrefab);
            inventoryItemViewInstance.InventoryItemModel = inventoryItemModel;
            return inventoryItemViewInstance;
        }

        private List<InventoryItemModel> GetInventoryItemModels()
        {
            var inventoryItemModels = new List<InventoryItemModel>();

            var automaticRifle = new InventoryItemModel(1,"AutomaticRifle", "Weapon", 5);
            inventoryItemModels.Add(automaticRifle);
            var food = new InventoryItemModel(2,"Apple", "Food", 1);
            inventoryItemModels.Add(food);
            var medkit = new InventoryItemModel(3,"FirstAidKit", "Medkit", 2);
            inventoryItemModels.Add(medkit);

            return inventoryItemModels;
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

        private void SetInventoryItemsPositions(List<InventoryItemView> inventoryItemViewInstances,
            Vector3 initialPosition, Vector3 positionStep)
        {
            for (var i = 0; i < inventoryItemViewInstances.Count; i++)
            {
                var inventoryItemViewInstance = inventoryItemViewInstances[i];
                inventoryItemViewInstance.gameObject.transform.position = initialPosition + positionStep * i;
            }
        }
    }
}
