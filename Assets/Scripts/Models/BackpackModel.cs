using System.Collections.Generic;
using Models.InventoryItems;
using Newtonsoft.Json;
using UnityEngine;

namespace Models
{
    public class BackpackModel : MonoBehaviour
    {
        private List<InventoryItemModel> _inventoryItemModels = new List<InventoryItemModel>();

        private const string _inventoryItemModelsKey = "InventoryItemModels";

        private void Awake()
        {
            Load();
        }

        public void AddItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModels.Add(inventoryItemModel);
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
