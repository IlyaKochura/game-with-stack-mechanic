using UnityEngine;

namespace Pool.Contracts
{
    public interface IObjectPool
    {
        public T Spawn<T>(GameObject prefab, Vector3 position, Quaternion quaternion, Transform parent = null)
            where T : Component, IRecycle;
    }
}