using System;
using System.Collections.Generic;
using UnityEngine;

namespace SUG.Essentials
{
    internal static class ServiceRegistry
    {
        private static readonly Dictionary<Type, object> _globalServices = new();

        private static readonly Dictionary<Type, object> _sceneServices = new();

        public static void RegisterGlobal(Type serviceType, object instance)
        {
            if (_globalServices.ContainsKey(serviceType))
            {
                Debug.LogWarning($"Global Service [{serviceType.Name}] 已经注册。");
                return;
            }

            _globalServices.Add(serviceType, instance);
        }

        public static void RegisterScene(Type serviceType, object instance)
        {
            if (_sceneServices.ContainsKey(serviceType))
            {
                Debug.LogWarning($"Scene Service [{serviceType.Name}] 已经注册。");
                return;
            }

            _sceneServices.Add(serviceType, instance);
        }

        public static object Resolve(Type type)
        {
            if (_sceneServices.TryGetValue(type, out var scene))
                return scene;

            if (_globalServices.TryGetValue(type, out var global))
                return global;

            return null;
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public static void ClearScene()
        {
            _sceneServices.Clear();
            //Debug.Log($"scene Services: {_sceneServices.Count}");
        }
    }
}