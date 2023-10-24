using Code.Stack.Components;
using Leopotam.EcsLite;
using Pool.Contracts;
using UnityEngine;

namespace Code.Stack
{
    public class StackAutoSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float DefaultTime = 1f;
        private const int MaxObjectCount = 14;
        
        private readonly GameObject _ballPrefab;
        private readonly IObjectPool _objectPool;
        private EcsPool<CStack> _stackPool;
        private float _time;
        private EcsFilter _stackFilter;

        public StackAutoSpawnSystem(GameObject ballPrefab, IObjectPool objectPool)
        {
            _ballPrefab = ballPrefab;
            _objectPool = objectPool;
        }
        
        public void Init(IEcsSystems systems)
        {
            _time = DefaultTime;
            
            var world = systems.GetWorld();

            _stackPool = world.GetPool<CStack>();
            _stackFilter = world.Filter<CStack>().End();
        }

        public void Run(IEcsSystems systems)
        {
            if (_time >= 0f)
            {
                _time -= Time.deltaTime;
                return;
            }

            _time = DefaultTime;

            foreach (var stack in _stackFilter)
            {
                ref var componentStack = ref _stackPool.Get(stack);

                if (!componentStack.activeSpawn)
                {
                    continue;
                }
                
                componentStack.stack ??= new();

                if (componentStack.stack.Count >= MaxObjectCount)
                {
                    return;
                }

                var posSpawn = componentStack.stack.Count == 0
                    ? componentStack.stackStartPos.position
                    : componentStack.stack.Peek().GetPositionForSpawn().position;

                 var stackObject = _objectPool.Spawn<Ball>(_ballPrefab, posSpawn,
                    Quaternion.identity, componentStack.stackStartPos);
                componentStack.stack.Push(stackObject);
            }
        }
    }
}