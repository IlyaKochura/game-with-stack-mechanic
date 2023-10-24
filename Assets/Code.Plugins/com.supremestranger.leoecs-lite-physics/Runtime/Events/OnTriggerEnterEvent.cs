using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnTriggerEnterEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collider collider;
    }
}