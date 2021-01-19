using System;
using System.Collections.Generic;
using System.Linq;
using Models.InventoryItems;
using UnityEngine;
using View.InventoryItems;

namespace Components
{
    [RequireComponent(typeof(InventoryItemView))]
    [RequireComponent(typeof(Renderer))]
    public class InventoryItemGameObjectMaterialSetterComponent : MonoBehaviour
    {
        [SerializeField]
        private List<TypeMaterial> _typeMaterials = new List<TypeMaterial>();

        [Serializable]
        public class TypeMaterial
        {
            public string Type;
            public Material Material;
        }

        private InventoryItemView _inventoryItemView;
        private Renderer _renderer;

        private void Awake()
        {
            _inventoryItemView = this.gameObject.GetComponent<InventoryItemView>();
            _renderer = this.gameObject.GetComponent<Renderer>();
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

            SetInventoryItemGameObjectMaterial(inventoryItemModel);
        }

        private void SetInventoryItemGameObjectMaterial(InventoryItemModel inventoryItemModel)
        {
            var rendererMaterial = _typeMaterials.FirstOrDefault(x => x.Type == inventoryItemModel.Type);
            if (rendererMaterial == null)
            {
                return;
            }

            var material = rendererMaterial.Material;

            _renderer.material = material;
        }
    }
}
