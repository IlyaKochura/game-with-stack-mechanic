using UnityEngine;

namespace Code.Input.Touchscreen
{
    public class FloatingJoystick : MonoBehaviour
    {
        [SerializeField] private RectTransform _joystickRectTransform;
        [SerializeField] private RectTransform _knob;

        public RectTransform JoystickRectTransform => _joystickRectTransform;
        public RectTransform Knob => _knob;
    }
}