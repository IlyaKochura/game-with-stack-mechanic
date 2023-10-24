using AB_Utility.FromSceneToEntityConverter;
using Code.Animation.Systems;
using Code.Input;
using Code.Movement;
using Code.Stack;
using Code.Stack.Components;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Pool;
using Pool.Contracts;
#if UNITY_EDITOR
using Leopotam.EcsLite.UnityEditor;
#endif
using UnityEngine;

namespace Code.EcsInit
{
    public class Initialize : MonoBehaviour
    {
        [SerializeField] private MovableInputProvider _movableInputProvider;
        [SerializeField] private GameObject _ballPrefab;

        EcsWorld _world;
        IEcsSystems _systems;
        private IObjectPool _objectPool;
        
        void Start()
        {
            if (_objectPool == null)
            {
                _objectPool = new ObjectPool();
            }
            
            DisableAutoRotation();
            Application.targetFrameRate = 60;
            
            _world = new EcsWorld ();
            EcsPhysicsEvents.ecsWorld = _world;
            _systems = new EcsSystems (_world);
            
            _world.ConvertSceneFromWorld();
            
            RegisterDebug();
            RegisterInput();
            RegisterMovement();
            RegisterStack();
            RegisterAnimation();
            
            _systems.Init ();
        }
        
        private void DisableAutoRotation() {
            Screen.autorotateToPortrait = false;
            Screen.autorotateToPortraitUpsideDown = false;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        void Update()
        {
            _systems?.Run ();
        }

        private void RegisterInput()
        {
            _systems.Add(new MoveInputSystem(_movableInputProvider));
        }

        private void RegisterMovement()
        {
            _systems.Add(new PlayerMovementSystem())
                .Add(new MovableSystem())
                .DelHere<CMoveInputEvent>();
        }

        private void RegisterDebug()
        {
#if UNITY_EDITOR
            _systems.Add(new EcsWorldDebugSystem())
                    .Add(new EcsSystemsDebugSystem());
#endif
        }

        private void RegisterStack()
        {
            _systems.Add(new StackInteractSystem());
            _systems.DelHerePhysics();
            _systems.Add(new StackSystem(_ballPrefab, _objectPool));
            _systems.DelHere<CStackInteract>();
            _systems.Add(new StackAutoSpawnSystem(_ballPrefab, _objectPool));
        }

        private void RegisterAnimation()
        {
            _systems.Add(new PlayerAnimationSystem(_movableInputProvider));
        }
        
        void OnDestroy()
        {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
            }
            
            if (_world != null) {
                _world.Destroy ();
                _world = null;
            }
        }
    }
}
