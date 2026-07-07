using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SUG.Essentials
{
    /// <summary>
    /// 自动扫描并注册所有 Service。
    /// </summary>
    internal static class ServiceScanner
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
            UnityEngine.SceneManagement.SceneManager.sceneUnloaded += OnSceneUnloaded;

            // 首次扫描全局服务
            ScanGlobal();
        }

        private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ScanScene(scene);
        }

        private static void OnSceneUnloaded(Scene scene)
        {
            ServiceRegistry.ClearScene();
        }

        #region Scan

        /// <summary>
        /// 扫描整个场景
        /// </summary>
        private static void ScanScene(Scene scene)
        {
            var roots = scene.GetRootGameObjects();

            foreach (var root in roots)
            {
                var behaviours = root.GetComponentsInChildren<MonoBehaviour>(true);

                foreach (var behaviour in behaviours)
                {
                    RegisterSceneService(behaviour);
                }
            }
        }

        /// <summary>
        /// 扫描所有全局对象
        /// </summary>
        private static void ScanGlobal()
        {
            var behaviours = UnityEngine.Object.FindObjectsByType<MonoBehaviour>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            foreach (var behaviour in behaviours)
            {
                RegisterGlobalService(behaviour);
            }
        }

        #endregion

        #region Register

        private static void RegisterSceneService(MonoBehaviour behaviour)
        {
            RegisterService(behaviour, typeof(ISceneService), true);
        }

        private static void RegisterGlobalService(MonoBehaviour behaviour)
        {
            RegisterService(behaviour, typeof(IGlobalService), false);
        }

        /// <summary>
        /// 注册一个 Service
        /// </summary>
        private static void RegisterService(MonoBehaviour behaviour, Type markerType, bool isScene)
        {
            var type = behaviour.GetType();

            var interfaces = ReflectionCache.GetInterfaces(type);

            bool isService = false;

            foreach (var i in interfaces)
            {
                if (i == markerType)
                {
                    isService = true;
                    break;
                }
            }

            if (!isService) return;

            foreach (var i in interfaces)
            {
                // 跳过 Marker Interface
                if (i == markerType) continue;

                if (isScene)
                {
                    ServiceRegistry.RegisterScene(i, behaviour);
                }
                else
                {
                    ServiceRegistry.RegisterGlobal(i, behaviour);
                }
            }

            foreach (var i in interfaces)
            {
                //Debug.Log($"  Interface : {i.Name}");
            }
        }

        #endregion
    }
}