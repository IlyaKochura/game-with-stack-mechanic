using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnTriggerStay2DEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collider2D collider2D;
    }
}