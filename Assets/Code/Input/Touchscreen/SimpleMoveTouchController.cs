using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace Code.Input.Touchscreen
{
    public class SimpleMoveTouchController : IMovableInputController
    {
        private readonly FloatingJoystick _floatingJoystick;
        public event Action<Vector2> OnMove;
        public event Action<bool> OnMovePressed;

        private Vector2Int _screenResolution;

        public SimpleMoveTouchController(FloatingJoystick floatingJoystick)
        {
            _floatingJoystick = floatingJoystick;

            _screenResolution = new Vector2Int(Screen.width, Screen.height);
        }
        
        private Finger _movementFinger;
        private Vector2 _movementAmount;
        
        public void Update()
        {
            
        }

        public void Init()
        {
            EnhancedTouchSupport.Enable();
            ETouch.Touch.onFingerUp += OnFingerUp;
            ETouch.Touch.onFingerDown += OnFingerDown;
            ETouch.Touch.onFingerMove += OnFingerMove;
        }

        private void OnFingerMove(Finger finger)
        {
            if (_movementFinger != finger)
            {
                return;
            }

            Vector2 knobPos;
            var maxMovement = _floatingJoystick.JoystickRectTransform.sizeDelta.x / 2f;
            var currentTouch = _movementFinger.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition,
                    _floatingJoystick.JoystickRectTransform.anchoredPosition) > maxMovement)
            {
                knobPos = (currentTouch.screenPosition - _floatingJoystick.JoystickRectTransform.anchoredPosition)
                    .normalized * maxMovement;
            }
            else
            {
                knobPos = currentTouch.screenPosition - _floatingJoystick.JoystickRectTransform.anchoredPosition;
            }

            _floatingJoystick.Knob.anchoredPosition = knobPos;
            _movementAmount = knobPos / maxMovement;
            
            OnMove?.Invoke(_movementAmount);
        }

        private void OnFingerUp(Finger finger)
        {
            if (_movementFinger != finger)
            {
                return;
            }

            _movementFinger = null;
            _floatingJoystick.Knob.anchoredPosition = Vector2.zero;
            _floatingJoystick.gameObject.SetActive(false);
            _movementAmount = Vector2.zero;
            
            OnMovePressed?.Invoke(false);
        }

        private void OnFingerDown(Finger finger)
        {
            if (_movementFinger != null && finger.screenPosition.x >= _screenResolution.x / 2f)
            {
                return;
            }
            
            _movementFinger = finger;
            _movementAmount = Vector2.zero;
            _floatingJoystick.gameObject.SetActive(true);
            _floatingJoystick.JoystickRectTransform.anchoredPosition = ClampStartPos(finger.screenPosition);
            OnMovePressed?.Invoke(true);
        }

        private Vector2 ClampStartPos(Vector2 fingerScreenPosition)
        {
            if (fingerScreenPosition.x < _floatingJoystick.JoystickRectTransform.sizeDelta.x / 2)
            {
                fingerScreenPosition.x = _floatingJoystick.JoystickRectTransform.sizeDelta.x / 2;
            }
            
            if (fingerScreenPosition.y < _floatingJoystick.JoystickRectTransform.sizeDelta.y / 2)
            {
                fingerScreenPosition.y = _floatingJoystick.JoystickRectTransform.sizeDelta.y / 2;
            }
            else if (fingerScreenPosition.y > Screen.height - _floatingJoystick.JoystickRectTransform.sizeDelta.y / 2)
            {
                fingerScreenPosition.y = Screen.height - _floatingJoystick.JoystickRectTransform.sizeDelta.y / 2;
            }

            return fingerScreenPosition;
        }

        public void Start()
        {
            
        }

        public void Disable()
        {
            ETouch.Touch.onFingerUp -= OnFingerUp;
            ETouch.Touch.onFingerDown -= OnFingerDown;
            ETouch.Touch.onFingerMove -= OnFingerMove;
            EnhancedTouchSupport.Disable();
        }

        public void Log()
        {
            Debug.LogError("ddddddd");
        }
    }
}