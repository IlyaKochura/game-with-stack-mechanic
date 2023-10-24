using System.Collections.Generic;
using Code.Stack.Components;
using Code.Stack.Contracts;
using Leopotam.EcsLite;
using Pool.Contracts;
using UnityEngine;

namespace Code.Stack
{
    public class StackSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsPool<CStack> _stackPool;
        private EcsFilter _interactFilter;
        private EcsPool<CStackInteract> _stackInteractPool;
        private readonly GameObject _ballPrefab;
        private readonly IObjectPool _objectPool;
        private float _time;

        public StackSystem(GameObject ballPrefab, IObjectPool objectPool)
        {
            _ballPrefab = ballPrefab;
            _objectPool = objectPool;
        }
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _stackPool = world.GetPool<CStack>();
            _stackInteractPool = world.GetPool<CStackInteract>();
            _interactFilter = world.Filter<CStackInteract>().Inc<CStack>().End();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var interact in _interactFilter)
            {
                ref var componentInteract = ref _stackInteractPool.Get(interact);
                ref var componentStack = ref _stackPool.Get(interact);
                ref var componentCollideStack = ref _stackPool.Get(componentInteract.interactEntityId);

                switch (componentInteract.interactVariant)
                {
                    case InteractVariants.Add :
                        
                        if (componentCollideStack.stack == null || componentCollideStack.stack.Count == 0) 
                        {
                            continue;
                        }
                        
                        StackProcess( ref componentCollideStack.stack,  ref componentStack.stack, componentStack.stackStartPos);

                        break;
                    
                    case InteractVariants.Get :
                        
                        if (componentStack.stack == null || componentStack.stack.Count == 0) 
                        {
                            continue;
                        }
                        
                        StackProcess( ref componentStack.stack,  ref componentCollideStack.stack, componentCollideStack.stackStartPos);

                        break;
                    
                    case InteractVariants.None :
                        break;

                }
            }
        }

        private void StackProcess(ref Stack<IStackObject> decrementStack, ref Stack<IStackObject> incrementStack, Transform startPoint)
        {
            Vector3 posSpawn;
            
            var obj = decrementStack.Pop();
            obj.Recycle();
            
            incrementStack ??= new();
            
            posSpawn = incrementStack.Count == 0
                ? startPoint.position
                : incrementStack.Peek().GetPositionForSpawn().position;
                        
            var stackObject = _objectPool.Spawn<Ball>(_ballPrefab, posSpawn,
                Quaternion.identity, startPoint);
            incrementStack.Push(stackObject);
        } 
        
    }
}