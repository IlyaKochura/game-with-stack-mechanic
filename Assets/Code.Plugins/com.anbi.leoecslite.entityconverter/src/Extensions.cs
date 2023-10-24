using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace AB_Utility.FromSceneToEntityConverter
{
    public static class Extensions
    {
        private static HashSet<EcsWorld> _dirtyWorlds = new HashSet<EcsWorld>();
        private static HashSet<ComponentsContainer> _dirtyObjects = new HashSet<ComponentsContainer>();

        public static IEcsSystems ConvertScene(this IEcsSystems systems)
        {
            var world = systems.GetWorld();
#if DEBUG
            if (_dirtyWorlds.Contains(world))
            {
                throw new Exception(
                    "You cannot convert systems with the same world twice <a href=\"https://safe-clove-478.notion.site/You-cannot-convert-systems-with-the-same-world-twice-6f4c638c18e74b0b91c65cd0c7f9dc7b\">more info</a>");
            }

            _dirtyWorlds.Add(world);
#endif
            var containers = UnityEngine.Object.FindObjectsOfType<ComponentsContainer>(true);

            for (int i = 0; i < containers.Length; i++)
            {
                var container = containers[i];
                EcsConverter.ConvertContainer(container, world);
            }

            return systems;
        }

        public static void ConvertSceneFromWorld(this EcsWorld ecsWorld)
        {
            var containers = UnityEngine.Object.FindObjectsOfType<ComponentsContainer>(true);

            foreach (var container in containers)
            {
                if (_dirtyObjects.Contains(container))
                {
#if DEBUG
                    Debug.Log($"{container.gameObject.name} has already been converted");
#endif
                    continue;
                }

                _dirtyObjects.Add(container);
                EcsConverter.ConvertContainer(container, ecsWorld);
            }
        }
    }
}