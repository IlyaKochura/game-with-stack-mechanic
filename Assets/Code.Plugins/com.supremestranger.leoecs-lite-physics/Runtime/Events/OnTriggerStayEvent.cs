using UnityEngine;

namespace LeoEcsPhysics
{
    public struct OnTriggerStayEvent
    {
        public int selfEntityId;
        public int collideEntityId;
        public GameObject TriggerGameObject;
        public GameObject senderGameObject;
        public Collider collider;
    }
}