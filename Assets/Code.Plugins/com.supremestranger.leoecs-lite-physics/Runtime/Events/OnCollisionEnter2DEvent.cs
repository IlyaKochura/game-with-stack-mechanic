using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnCollisionEnter2DEvent
    {
        public int entityId;
        public GameObject senderGameObject;
        public Collision2D collision2D;
    }
}