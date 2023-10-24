using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Movement
{
    public class MovableSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<CMovable> _movablePool;
        private EcsPool<CTransform> _transformPool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _filter = world.Filter<CMovable>().Inc<CTransform>().End();
            _movablePool = world.GetPool<CMovable>();
            _transformPool = world.GetPool<CTransform>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var transform = ref _transformPool.Get(entity);
                ref var movable = ref _movablePool.Get(entity);
                
                transform.transform.position += movable.velocity;
                
                if (movable.velocity.magnitude > movable.maxSpeed)
                {
                    movable.velocity.Normalize();
                    movable.velocity *= movable.maxSpeed;
                }

                movable.velocity *= movable.damping;
                
                transform.transform.LookAt(transform.transform.position + movable.velocity, Vector3.up);
            }
        }
    }
}