using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnCollisionExit2DChecker : BasePhysicsChecker
    {
        private void OnCollisionExit2D(Collision2D other)
        {
            EcsPhysicsEvents.RegisterCollisionExit2DEvent(gameObject, other.collider, other.relativeVelocity, entity);
        }
    }
}
