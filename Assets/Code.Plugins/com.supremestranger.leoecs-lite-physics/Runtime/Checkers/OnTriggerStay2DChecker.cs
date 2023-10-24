using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnTriggerStay2DChecker : BasePhysicsChecker
    {
        private void OnTriggerStay2D(Collider2D other)
        { 
            EcsPhysicsEvents.RegisterTriggerStay2DEvent(gameObject, other, entity);
        }
    }
}