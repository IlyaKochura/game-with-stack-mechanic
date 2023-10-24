using System;
using UnityEngine;

namespace Code.Input
{
    public interface IMovableInputController
    {
        public event Action<Vector2> OnMove;
        public event Action<bool> OnMovePressed;
        public void Update();
        public void Init();
        public void Start();
        public void Disable();
    }
}