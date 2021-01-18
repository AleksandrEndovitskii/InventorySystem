using System.Collections.Generic;
using Models.InventoryItems;
using UnityEngine;

namespace Models
{
    public class BackpackModel : MonoBehaviour
    {
        private List<InventoryItemModel> _inventoryItemModels = new List<InventoryItemModel>();

        public void AddItem(InventoryItemModel inventoryItemModel)
        {
            _inventoryItemModels.Add(inventoryItemModel);
        }
    }
}
