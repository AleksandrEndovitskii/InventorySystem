using System;
using System.Collections.Generic;
using Components;
using Components.InventoryItemComponents;
using Models.InventoryItems;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
    [RequireComponent(typeof(InventoryItemsOnCollisionEnterBakcpackAddingComponent))]
    public class BackpackModel : MonoBehaviour
    {
        public Action<InventoryItemModel> InventoryItemModelAdded = delegate {  };
        public Action<InventoryItemModel> InventoryItemModelRemoved = delegate {  };

        public List<InventoryItemModel> InventoryItemModels
        {
            get
            {
                return _inventoryItemModels;
            }
            set
            {
                _inventoryItemModels = value;
            }
        }

        private List<InventoryItemModel> _inventoryItemModels = new List<InventoryItemModel>();

        private const string _inventoryItemModelsKey = "InventoryItemModels";

        private void Awake()
        {
            var inventoryItemModels = Load();
            foreach (var inventoryItemModel in inventoryItemModels)
            {
                AddItem(inventoryItemModel);
            }
        }

        public void AddItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModels.Add(inventoryItemModel);

            Debug.Log($"InventoryItemModel({inventoryItemModel}) was added to " +
                      $"InventoryItemModels({_inventoryItemModels}) started");

            InventoryItemModelAdded.Invoke(inventoryItemModel);

            Save(_inventoryItemModelsKey, _inventoryItemModels);
        }
        public void RemoveItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModels.Remove(inventoryItemModel);

            Debug.Log($"InventoryItemModel({inventoryItemModel}) was removed to " +
                      $"InventoryItemModels({_inventoryItemModels}) started");

            InventoryItemModelRemoved.Invoke(inventoryItemModel);

            Save(_inventoryItemModelsKey, _inventoryItemModels);
        }

        private void Save(string inventoryItemModelsKey, List<InventoryItemModel> inventoryItemModels)
        {
            var jsonString = JsonConvert.SerializeObject(inventoryItemModels);

            Debug.Log($"Saving of key({inventoryItemModelsKey}) with value({jsonString}) started");

            PlayerPrefs.SetString(inventoryItemModelsKey, jsonString);

            Debug.Log($"Saving of key({inventoryItemModelsKey}) with value({jsonString}) finished");
        }
        private List<InventoryItemModel> Load()
        {
            Debug.Log($"Loading of key({_inventoryItemModelsKey}) started");

            var inventoryItemModels = new List<InventoryItemModel>();
            var inventoryItemModelsDefaultValue = JsonConvert.SerializeObject(inventoryItemModels);
            var jsonString = PlayerPrefs.GetString(_inventoryItemModelsKey, inventoryItemModelsDefaultValue);
            // list of inventory items can contain null items
            inventoryItemModels = JsonConvert.DeserializeObject<List<InventoryItemModel>>(jsonString);
            // clear null items from list of inventory items
            inventoryItemModels.RemoveAll(x => x == null);

            Debug.Log($"Loading of key({_inventoryItemModelsKey}) with value({jsonString}) finished");

            return inventoryItemModels;
        }
    }
}
