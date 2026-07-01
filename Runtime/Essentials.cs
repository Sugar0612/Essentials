using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using VContainer;
using VContainer.Unity;
using static UnityEditor.ObjectChangeEventStream;

namespace SUG.Essentials
{
    public static class Essentials
    {
        public static EssentialsSettingsSO Settings { get; private set; }

        public static IObjectResolver Container { get; private set; }

        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            // 创建实例，为后续初始化做准备
            var root = new GameObject("[Essentials]");
            Object.DontDestroyOnLoad(root);

            // 作为MonoBehaviour桥梁，可GameObject.Instiante
            //RuntimeBridge.Initialize();

            // 加载Essentials总配置表
            Settings = Resources.Load<EssentialsSettingsSO>("Essentials/Bootstrap");

            // 给场景中的所有MonoBehaviour脚本都通过VContainer注册
            var scope = root.AddComponent<EssentialsLifetimeScope>();
            SceneInjector.Initialize(); // 依赖于继承了LifetimeScope的Container成员
        }

        public static void Inject(object target)
        {
            Container?.Inject(target);
        }

        internal static void SetContainer(IObjectResolver resolver)
        {
            Container = resolver;
        }

        // 场景实例化
        public static T Instantiate<T>(T prefab) where T : Object
        {
            var obj = Object.Instantiate(prefab);

            Inject(obj);

            return obj;
        }

        public static void Inject(Object obj)
        {
            if (obj is GameObject go)
            {
                InjectGameObject(go);
                return;
            }

            if (obj is Component component)
            {
                Container.Inject(component);
            }
        }

        private static void InjectGameObject(GameObject go)
        {
            var monos = go.GetComponentsInChildren<MonoBehaviour>(true);

            foreach (var mono in monos)
            {
                if (mono == null) continue;
                Container
                    .Inject(mono);
            }
        }
    }
}