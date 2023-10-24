using Code.Animation.Components;
using Code.Input;
using Code.Movement;
using Leopotam.EcsLite;

namespace Code.Animation.Systems
{
    public class PlayerAnimationSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly MovableInputProvider _inputProvider;
        private EcsFilter _animationFilter;
        private EcsPool<CAnimation> _animationPool;

        public PlayerAnimationSystem(MovableInputProvider inputProvider)
        {
            _inputProvider = inputProvider;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _animationFilter = world.Filter<CAnimation>().Inc<CPlayerControlled>().End();
            _animationPool = world.GetPool<CAnimation>();
            
            _inputProvider.OnMove += OnMove;
        }

        private void OnMove(bool b)
        {
            foreach (var animation in _animationFilter)
            {
                ref var componentAnimation = ref _animationPool.Get(animation);
                
                if (b)
                {
                    componentAnimation.animator.Play("Walking");
                    continue;
                }
                
                componentAnimation.animator.Play("Idle");
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            _inputProvider.OnMove -= OnMove;
        }
    }
}