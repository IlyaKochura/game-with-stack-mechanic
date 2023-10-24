using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnCollisionStayChecker : BasePhysicsChecker
    {
        private void OnCollisionStay(Collision other)
        {
            EcsPhysicsEvents.RegisterCollisionStayEvent(gameObject, other.collider, other.GetContact(0), other.relativeVelocity, entity);
        }
    }
}