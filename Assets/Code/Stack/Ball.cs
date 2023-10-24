using Code.Stack.Contracts;

using UnityEngine;

namespace Code.Stack
{
    public class Ball : MonoBehaviour, IStackObject
    {
        [SerializeField] private Transform _spawnPos;

        public Transform GetPositionForSpawn()
        {
            return _spawnPos;
        }
        
        public void Recycle()
        {
            gameObject.SetActive(false);
        }
    }
}