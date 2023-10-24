using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnTriggerExitEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collider collider;
    }
}