using System;
using System.Collections.Generic;
using Code.Input.Touchscreen;
using UnityEngine;

namespace Code.Input
{
    public class MovableInputProvider : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick _floatingJoystick;
        public Action<bool> OnMove { get; set; }

        private List<IMovableInputController> _movableInputControllers;
        public bool IsMovePressed { get; private set; }
        public Vector2 Direction { get; private set; }
        
        public void Awake()
        {
            _movableInputControllers = new List<IMovableInputController>();
            
            _movableInputControllers.Add(new SimpleMoveTouchController(_floatingJoystick));

            foreach (var controller in _movableInputControllers)
            {
                controller.OnMovePressed += MovePressed;
                controller.OnMove += UpdateDirection;
                controller.Init();
            }
        }

        public void Start()
        {
            foreach (var controller in _movableInputControllers)
            {
                controller?.Start();
            }
        }
        
        private void MovePressed(bool isMovePressed)
        {
            IsMovePressed = isMovePressed;
            OnMove?.Invoke(isMovePressed);
        }

        private void UpdateDirection(Vector2 direction)
        {
            Direction = direction;
        }

        public void Update()
        {
            foreach (var controller in _movableInputControllers)
            {
                controller?.Update();
            }
        }

        public void OnDestroy()
        {
            foreach (var controller in _movableInputControllers)
            {
                controller.OnMovePressed -= MovePressed;
                controller.OnMove -= UpdateDirection;
                controller.Disable();
            }
        }
    }
}