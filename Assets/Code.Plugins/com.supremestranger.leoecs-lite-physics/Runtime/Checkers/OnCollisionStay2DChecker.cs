using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnCollisionStay2DChecker : BasePhysicsChecker
    {
        private void OnCollisionStay2D(Collision2D other)
        {
            EcsPhysicsEvents.RegisterCollisionStay2DEvent(gameObject, other.collider, other.GetContact(0), other.relativeVelocity, entity);
        }
    }
}