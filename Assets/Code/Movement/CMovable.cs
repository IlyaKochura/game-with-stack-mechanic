using System;
using UnityEngine;

namespace Code.Movement
{
    [Serializable]
    public struct CMovable
    {
        public Vector3 velocity;
        public float damping;
        public float maxSpeed;
    }
}