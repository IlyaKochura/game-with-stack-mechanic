using System;
using System.Collections.Generic;
using Code.Stack.Contracts;
using UnityEngine;

namespace Code.Stack.Components
{
    [Serializable]
    public struct CStack
    {
        public bool adderPool;
        public Stack<IStackObject> stack;
        public bool activeSpawn;
        public Transform stackStartPos;
    }
}