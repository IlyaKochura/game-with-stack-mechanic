using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnTriggerEnterChecker : BasePhysicsChecker
    {
        private void OnTriggerEnter(Collider other)
        { 
            EcsPhysicsEvents.RegisterTriggerEnterEvent(gameObject, other, entity);
        }
    }
}
