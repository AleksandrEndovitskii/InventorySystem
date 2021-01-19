using Models.InventoryItems;
using UnityEngine;

namespace View.InventoryItems
{
    public class InventoryItemView : MonoBehaviour
    {
        public InventoryItemModel InventoryItemModel;

        public void SetModel(InventoryItemModel inventoryItemModel)
        {
            InventoryItemModel = inventoryItemModel;
        }
    }
}
