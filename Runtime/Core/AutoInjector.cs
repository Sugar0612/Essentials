using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SUG.Essentials
{
    internal static class AutoInjector
    {
        public static void Initialize()
        {
            SceneManager.sceneLoaded += InjectScene;
        }

        private static void InjectScene(Scene scene, LoadSceneMode mode)
        {
            var roots = scene.GetRootGameObjects();

            foreach (var root in roots)
            {
                var behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);

                foreach (var behaviour in behaviours)
                {
                    if (behaviour == null)
                        continue;

                    // 没有 [EInject] 字段就直接跳过
                    if (!HasInjectField(behaviour.GetType()))
                        continue;

                    Injector.Inject(behaviour);
                }
            }
        }

        private static bool HasInjectField(Type type)
        {
            var fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                if (Attribute.IsDefined(field, typeof(EInjectAttribute)))
                    return true;
            }

            return false;
        }
    }
}