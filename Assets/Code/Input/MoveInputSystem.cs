using Leopotam.EcsLite;

namespace Code.Input
{
    public class MoveInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly MovableInputProvider _movableInputProvider;
        private EcsWorld _world;
        private EcsPool<CMoveInputEvent> _moveInputPool;

        public MoveInputSystem(MovableInputProvider movableInputProvider)
        {
            _movableInputProvider = movableInputProvider;
        }
    
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
            _moveInputPool = _world.GetPool<CMoveInputEvent>();
        }
    
        public void Run(IEcsSystems systems)
        {
            if (!_movableInputProvider.IsMovePressed)
            {
                return;
            }

            var entity = _world.NewEntity();
            ref var moveInput = ref _moveInputPool.Add(entity);
            moveInput.Direction = _movableInputProvider.Direction;
        }
    }
}