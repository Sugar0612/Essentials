using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SUG.Essentials
{
    internal static class Injector
    {
        public static void InjectScene(Scene scene)
        {
            // Debug.Log($"=========== 注入 InjectScene {scene.name}");
            var roots = scene.GetRootGameObjects();

            foreach (var root in roots)
            {
                var behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);

                foreach (var behaviour in behaviours)
                {
                    if (behaviour == null) continue;

                    // 没有 [EInject] 字段就直接跳过
                    if (!HasInjectField(behaviour.GetType())) continue;

                    Inject(behaviour);
                }
            }
        }

        // Toolkit
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

        // Core
        public static void Inject(object target)
        {
            if (target == null) return;

            var type = target.GetType();

            var fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);

            foreach (var field in fields)
            {
                if (!Attribute.IsDefined(field, typeof(EInjectAttribute))) continue;

                var service = ServiceRegistry.Resolve(field.FieldType);

                if (service == null) continue;

                field.SetValue(target, service);
            }
        }
    }
}