using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnCollisionEnterEvent
    {
        public int selfEntityId;
        public int collideEntityId;
        public GameObject senderGameObject;
        public Collision collision;
        public GameObject gameObject;
    }
}