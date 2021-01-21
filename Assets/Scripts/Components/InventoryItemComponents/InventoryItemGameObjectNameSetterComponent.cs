using Models.InventoryItems;
using UnityEngine;
using View.InventoryItems;

namespace Components.InventoryItemComponents
{
    [RequireComponent(typeof(InventoryItemView))]
    public class InventoryItemGameObjectNameSetterComponent : MonoBehaviour
    {
        private InventoryItemView _inventoryItemView;

        private void Awake()
        {
            _inventoryItemView = this.gameObject.GetComponent<InventoryItemView>();
        }
        private void Start()
        {
            OnInventoryItemModelChanged(_inventoryItemView.InventoryItemModel);
            _inventoryItemView.InventoryItemModelChanged += OnInventoryItemModelChanged;
        }
        private void OnDestroy()
        {
            _inventoryItemView.InventoryItemModelChanged -= OnInventoryItemModelChanged;
        }

        private void OnInventoryItemModelChanged(InventoryItemModel inventoryItemModel)
        {
            if (inventoryItemModel == null)
            {
                return;
            }

            SetInventoryItemGameObjectName(inventoryItemModel);
        }

        private void SetInventoryItemGameObjectName(InventoryItemModel inventoryItemModel)
        {
            this.gameObject.name = inventoryItemModel.Name;
        }
    }
}
