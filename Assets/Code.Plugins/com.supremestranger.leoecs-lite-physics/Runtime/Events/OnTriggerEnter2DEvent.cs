using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnTriggerEnter2DEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collider2D collider2D;
    }
}