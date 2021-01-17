using UnityEngine;

namespace Components
{
    public class CollisionDetectionComponent : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log($"{this.gameObject.name} invoked OnCollisionEnter with {other.gameObject.name}");
        }
        private void OnCollisionExit(Collision other)
        {
            Debug.Log($"{this.gameObject.name} invoked OnCollisionExit with {other.gameObject.name}");
        }
    }
}
