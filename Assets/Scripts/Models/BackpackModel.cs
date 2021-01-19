using System.Collections.Generic;
using Components;
using Models.InventoryItems;
using Newtonsoft.Json;
using UnityEngine;
using View.InventoryItems;

namespace Models
{
    [RequireComponent(typeof(InventoryItemsSlotSetterComponent))]
    public class BackpackModel : MonoBehaviour
    {
        private List<InventoryItemModel> _inventoryItemModels = new List<InventoryItemModel>();

        private const string _inventoryItemModelsKey = "InventoryItemModels";

        private InventoryItemsSlotSetterComponent _inventoryItemsSlotSetterComponent;

        private void Awake()
        {
            _inventoryItemsSlotSetterComponent = this.gameObject.GetComponent<InventoryItemsSlotSetterComponent>();

            Load();
        }
        private void Start()
        {
            _inventoryItemsSlotSetterComponent.InventoryItemAttached += OnInventoryItemAttached;
            _inventoryItemsSlotSetterComponent.InventoryItemDetached += OnInventoryItemDetached;
        }
        private void OnDestroy()
        {
            _inventoryItemsSlotSetterComponent.InventoryItemAttached -= OnInventoryItemAttached;
            _inventoryItemsSlotSetterComponent.InventoryItemDetached -= OnInventoryItemDetached;
        }

        private void OnInventoryItemAttached(InventoryItemView inventoryItemView)
        {

        }
        private void OnInventoryItemDetached(InventoryItemView inventoryItemView)
        {

        }

        public void AddItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModels.Add(inventoryItemModel);

            Save();
        }
        public void RemoveItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModels.Remove(inventoryItemModel);

            Save();
        }

        private void Save()
        {
            var jsonString = JsonConvert.SerializeObject(_inventoryItemModels);

            Debug.Log($"Saving of key({_inventoryItemModelsKey}) with value({jsonString}) started");

            PlayerPrefs.SetString(_inventoryItemModelsKey, jsonString);

            Debug.Log($"Saving of key({_inventoryItemModelsKey}) with value({jsonString}) finished");
        }
        private void Load()
        {
            Debug.Log($"Loading of key({_inventoryItemModelsKey}) started");

            var inventoryItemModels = new List<InventoryItemModel>();
            var inventoryItemModelsDefaultValue = JsonConvert.SerializeObject(inventoryItemModels);
            var jsonString = PlayerPrefs.GetString(_inventoryItemModelsKey, inventoryItemModelsDefaultValue);
            _inventoryItemModels = JsonConvert.DeserializeObject<List<InventoryItemModel>>(jsonString);

            Debug.Log($"Loading of key({_inventoryItemModelsKey}) with value({jsonString}) finished");
        }
    }
}
