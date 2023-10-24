using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnCollisionStayEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collider collider;
        public ContactPoint firstContactPoint;
        public Vector3 relativeVelocity;
    }
}