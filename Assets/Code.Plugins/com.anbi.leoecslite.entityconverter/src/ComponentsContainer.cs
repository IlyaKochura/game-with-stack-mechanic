using System.Linq;
using Code.Plugins.com.supremestranger.leoecs_lite_physics.Runtime.Contracts;
using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter
{
    [DisallowMultipleComponent]
    public class ComponentsContainer : MonoBehaviour
    {
        [SerializeField] private int entity;
        [SerializeField] private BaseConverter[] _converters;
        [SerializeField] private BasePhysicsChecker[] _checkers;
        [SerializeField] private bool _destroyAfterConversion;
        
        public BaseConverter[] Converters => _converters;
        public bool DestroyAfterConversion => _destroyAfterConversion;

        internal void GetConverters()
        {
            _converters = GetComponents<BaseConverter>();
        }

        internal void SaveEntity(int entity)
        {
            this.entity = entity;
            if (!_checkers.Any())
            {
                return;
            }

            foreach (var checker in _checkers)
            {
                checker.BindEntity(entity);
            }
        }
    }
}