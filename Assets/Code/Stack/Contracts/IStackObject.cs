using Pool.Contracts;
using UnityEngine;

namespace Code.Stack.Contracts
{
    public interface IStackObject : IRecycle
    {
        Transform GetPositionForSpawn();
    }
}
