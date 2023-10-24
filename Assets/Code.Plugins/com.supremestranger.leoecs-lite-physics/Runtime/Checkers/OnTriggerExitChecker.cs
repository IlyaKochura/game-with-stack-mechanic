using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace LeoEcsPhysics
{
    public class OnTriggerExitChecker : BasePhysicsChecker
    {
        private void OnTriggerExit(Collider other)
        {
            EcsPhysicsEvents.RegisterTriggerExitEvent(gameObject, other, entity);
        }
    }
}