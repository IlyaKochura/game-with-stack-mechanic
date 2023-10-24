using System;
using System.Collections.Generic;
using System.Linq;
using Pool.Contracts;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool
{
    public class ObjectPool : IObjectPool
    {
        private Dictionary<Type, List<GameObject>> _objectsPool = new Dictionary<Type, List<GameObject>>();

        private void AddToPool<T>(GameObject obj) where T : Component, IRecycle
        {
            Type objType = typeof(T);
            if (_objectsPool.ContainsKey(objType))
            {
                _objectsPool[objType].Add(obj);
            }
            else
            {
                List<GameObject> newPool = new List<GameObject>();
                newPool.Add(obj);
                _objectsPool.Add(objType, newPool);
            }
        }
        
        public T Spawn<T>(GameObject prefab , Vector3 position, Quaternion quaternion, Transform parent = null) where T : Component, IRecycle
        {
            var obj = GetInactiveObject<T>();
            if (obj != null)
            {
                obj.transform.position = position;
                obj.transform.rotation = quaternion;
                obj.SetActive(true);
                if (parent != null)
                {
                    obj.transform.SetParent(parent);
                }
                return obj.GetComponent<T>();
            }
            else
            {
                obj = Object.Instantiate(prefab, position, quaternion);
                AddToPool<T>(obj);
                if (parent != null)
                {
                    obj.transform.SetParent(parent);
                }
                return obj.GetComponent<T>();
            }
        }

        private GameObject GetInactiveObject<T>() where T : Component, IRecycle
        {
            Type objType = typeof(T);
            if (_objectsPool.ContainsKey(objType))
            {
                var pool = _objectsPool[objType];
                if (pool.All(x => x.activeSelf))
                {
                    return null;
                }
                return pool.First(x => !x.activeSelf);
            }
            return null;
        }
        
        // public List<GameObject> GetActiveObject<T>() where T : Component, IRecycle
        // {
        //     Type objType = typeof(T);
        //     if (_objectsPool.ContainsKey(objType))
        //     {
        //         var pool = _objectsPool[objType];
        //         return pool.Where(x => x.activeSelf).ToList();
        //     }
        //     return null;
        // }
    }
}