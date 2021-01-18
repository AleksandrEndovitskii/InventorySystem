using System;
using UnityEngine;

namespace Components
{
    public class CollisionDetectionComponent : MonoBehaviour
    {
        public Action<Collision> CollisionEnter = delegate { };
        public Action<Collision> CollisionExit = delegate { };

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
