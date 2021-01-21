using Models;
using UnityEngine;
using View.InventoryItems;

namespace Components.InventoryItemComponents
{
    [RequireComponent(typeof(CollisionDetectionComponent))]
    [RequireComponent(typeof(BackpackModel))]
    public class InventoryItemsOnCollisionEnterBakcpackAddingComponent : MonoBehaviour
    {
        private BackpackModel _backpackModel;
        private CollisionDetectionComponent _collisionDetectionComponent;

        private void Awake()
        {
            _backpackModel = this.gameObject.GetComponent<BackpackModel>();
            _collisionDetectionComponent = this.gameObject.GetComponent<CollisionDetectionComponent>();
        }
        private void Start()
        {
            _collisionDetectionComponent.CollisionEnter += OnCollisionEnter;
            _collisionDetectionComponent.CollisionExit += OnCollisionExit;
        }
        private void OnDestroy()
        {
            _collisionDetectionComponent.CollisionEnter -= OnCollisionEnter;
            _collisionDetectionComponent.CollisionExit -= OnCollisionExit;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var inventoryItemView = collision.gameObject.GetComponent<InventoryItemView>();
            if (inventoryItemView == null)
            {
                return;
            }

            _backpackModel.AddItem(inventoryItemView.InventoryItemModel);
        }
        private void OnCollisionExit(Collision collision)
        {

        }
    }
}
