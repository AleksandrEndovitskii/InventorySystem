using System.Collections.Generic;
using Components;
using Models.InventoryItems;
using Newtonsoft.Json;
using UnityEngine;
using View.InventoryItems;

namespace Models
{
    [RequireComponent(typeof(ChildSetterComponent))]
    public class BackpackModel : MonoBehaviour
    {
        private List<InventoryItemModel> _inventoryItemModels = new List<InventoryItemModel>();

        private const string _inventoryItemModelsKey = "InventoryItemModels";

        private ChildSetterComponent _childSetterComponent;

        private void Awake()
        {
            _childSetterComponent = this.gameObject.GetComponent<ChildSetterComponent>();

            Load();
        }
        private void Start()
        {
            _childSetterComponent.InventoryItemAttached += OnInventoryItemAttached;
            _childSetterComponent.InventoryItemDetached += OnInventoryItemDetached;
        }
        private void OnDestroy()
        {
            _childSetterComponent.InventoryItemAttached -= OnInventoryItemAttached;
            _childSetterComponent.InventoryItemDetached -= OnInventoryItemDetached;
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
