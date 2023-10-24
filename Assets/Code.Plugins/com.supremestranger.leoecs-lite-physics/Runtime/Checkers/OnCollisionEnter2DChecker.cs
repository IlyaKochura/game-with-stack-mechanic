using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnCollisionEnter2DChecker : BasePhysicsChecker
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            EcsPhysicsEvents.RegisterCollisionEnter2DEvent(gameObject, other, entity);
        }
    }
}