using Code.Stack.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace Code.Stack
{
    public class StackInteractSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _interactEntitiesFilter;
        private EcsPool<OnTriggerStayEvent> _interactEventPool;
        private EcsPool<CStackInteract> _stackInteractPool;
        private EcsPool<CStack> _stackPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _stackInteractPool = world.GetPool<CStackInteract>();
            _stackPool = world.GetPool<CStack>();
            _interactEventPool = world.GetPool<OnTriggerStayEvent>();
            _interactEntitiesFilter = world.Filter<OnTriggerStayEvent>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var interact in _interactEntitiesFilter)
            {
                ref var collisionData = ref _interactEventPool.Get(interact);

                if (!_stackPool.Has(collisionData.selfEntityId) || _stackInteractPool.Has(collisionData.selfEntityId))
                {
                    continue;
                }

                _stackInteractPool.Add(collisionData.selfEntityId);
                
                ref var componentInteract = ref _stackInteractPool.Get(collisionData.selfEntityId);

                componentInteract.interactEntityId = collisionData.collideEntityId;
                
                ref var componentStackSelf = ref _stackPool.Get(collisionData.selfEntityId);
                
                switch (collisionData.TriggerGameObject.tag)
                {
                    case "Player" :

                        if (componentStackSelf.adderPool)
                        {
                            componentInteract.interactVariant = InteractVariants.Get;
                            break;
                        }

                        componentInteract.interactVariant = InteractVariants.Add;
                        break;
                    
                    case "AdderStack" :
                        componentInteract.interactVariant = InteractVariants.Add;
                        break;
                    
                    case "GetterStack" :
                        componentInteract.interactVariant = InteractVariants.Get;
                        break;
                    
                    default:
                        break;
                    
                }
            }
        }
    }
}