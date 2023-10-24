using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnCollisionExitEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collider collider;
        public Vector3 relativeVelocity;
    }
}