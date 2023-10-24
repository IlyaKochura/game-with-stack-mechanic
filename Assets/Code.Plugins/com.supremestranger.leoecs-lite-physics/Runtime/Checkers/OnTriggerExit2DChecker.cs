using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnTriggerExit2DChecker : BasePhysicsChecker
    {
        private void OnTriggerExit2D(Collider2D other)
        { 
            EcsPhysicsEvents.RegisterTriggerExit2DEvent(gameObject, other, entity);
        }
    }
}