using System;
using Models.InventoryItems;
using UnityEngine;

namespace View.InventoryItems
{
    public class InventoryItemView : MonoBehaviour
    {
        public Action<InventoryItemModel> InventoryItemModelChanged = delegate {  };

        public InventoryItemModel InventoryItemModel
        {
            get
            {
                return _inventoryItemModel;
            }
            set
            {
                if (value == _inventoryItemModel)
                {
                    return;
                }

                _inventoryItemModel = value;

                Debug.Log("InventoryItemModelChanged");

                InventoryItemModelChanged.Invoke(_inventoryItemModel);
            }
        }

        private InventoryItemModel _inventoryItemModel;
    }
}
