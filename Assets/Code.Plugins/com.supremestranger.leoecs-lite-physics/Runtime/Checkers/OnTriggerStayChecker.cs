using System;
using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Container;
using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnTriggerStayChecker : BasePhysicsChecker
    {
        private void OnTriggerStay(Collider other)
        {
            var otherComponent = other.gameObject.GetComponent<EntityContainer>();

            if (otherComponent)
            {
                EcsPhysicsEvents.RegisterTriggerStayEvent(gameObject, other, entity, otherComponent.EntityId);   
            }
        }
    }
}