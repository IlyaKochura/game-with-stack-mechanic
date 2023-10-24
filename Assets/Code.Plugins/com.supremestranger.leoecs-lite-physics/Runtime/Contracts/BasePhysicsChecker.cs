using UnityEngine;

namespace Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts
{
    public class BasePhysicsChecker : MonoBehaviour, IEcsPhysicsChecker
    {
        protected int entity;
        
        public void BindEntity(int entity)
        {
            this.entity = entity;
        }
    }
}