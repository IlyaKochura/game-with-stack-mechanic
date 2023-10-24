using Code.Input;
using Leopotam.EcsLite;
using UnityEngine;

namespace Code.Movement
{
    public class PlayerMovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<CMoveInputEvent> _moveInputPool;
        private EcsFilter _moveInputFilter;
        private EcsPool<CMovable> _movablePool;
        private EcsFilter _playerControlledFilter;
        private EcsPool<CMoveEngine> _moveEnginePool;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _moveInputFilter = world.Filter<CMoveInputEvent>().End();
            _playerControlledFilter = world.Filter<CPlayerControlled>().Inc<CMovable>().Inc<CMoveEngine>().End();
            
            _moveInputPool = world.GetPool<CMoveInputEvent>();
            _moveEnginePool = world.GetPool<CMoveEngine>();
            _movablePool = world.GetPool<CMovable>();
        }
    
        public void Run(IEcsSystems systems)
        {
            foreach (var inputEntity in _moveInputFilter)
            {
                foreach (var entity in _playerControlledFilter)
                {
                    ref var movable = ref _movablePool.Get(entity);
                    var moveEngine = _moveEnginePool.Get(entity);
                    var input = _moveInputPool.Get(inputEntity);

                    Vector3 direction = new Vector3(input.Direction.x, 0, input.Direction.y);
                    var temp = direction  * (moveEngine.force * Time.deltaTime);
                    movable.velocity += temp;
                }
            }
        }
    }
}