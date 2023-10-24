using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnCollisionEnterChecker : BasePhysicsChecker
    {
        private void OnCollisionEnter(Collision other)
        {
            var otherComponent = other.gameObject.GetComponent<OnCollisionEnterChecker>();

            if (otherComponent)
            {
                EcsPhysicsEvents.RegisterCollisionEnterEvent(gameObject, other, entity, otherComponent.entity);   
            }
        }
    }
}
