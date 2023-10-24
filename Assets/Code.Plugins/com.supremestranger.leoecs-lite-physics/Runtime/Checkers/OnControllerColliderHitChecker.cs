using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnControllerColliderHitChecker : BasePhysicsChecker
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            EcsPhysicsEvents.RegisterControllerColliderHitEvent(gameObject, hit.collider, hit.normal, hit.moveDirection, entity);
        }
    }
}