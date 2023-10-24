using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnTriggerEnter2DChecker : BasePhysicsChecker
    {
        private void OnTriggerEnter2D(Collider2D other)
        { 
            EcsPhysicsEvents.RegisterTriggerEnter2DEvent(gameObject, other, entity);
        }
    }
}