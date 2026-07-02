using UnityEngine;

namespace SUG.Essentials
{
    public static class Essentials
    {
        public static EssentialsSettingsSO Settings { get; private set; }

        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            // 创建实例，为后续初始化做准备
            var root = new GameObject("[Essentials]");
            Object.DontDestroyOnLoad(root);

            // 加载Essentials总配置表
            Settings = Resources.Load<EssentialsSettingsSO>("Essentials/Bootstrap");

            ServiceScanner.Initialize();

            AutoInjector.Initialize();
        }

        //public static void Inject(object target) => Injector.Inject(target);

        //internal static void SetContainer(IObjectResolver resolver) => GlobalResolver = resolver;

        public static T Resolve<T>()
        {
            return ServiceRegistry.Resolve<T>();
        }

        public static object Resolve(System.Type type)
        {
            return ServiceRegistry.Resolve(type);
        }

        // 场景实例化
        public static T Instantiate<T>(T prefab) where T : Object
        {
            var obj = Object.Instantiate(prefab);

            Inject(obj);

            return obj;
        }

        private static void Inject(Object obj)
        {
            if (obj is GameObject go)
            {
                InjectGameObject(go);
                return;
            }

            if (obj is Component component)
            {
                Injector.Inject(component);
            }
        }

        private static void InjectGameObject(GameObject go)
        {
            var monos = go.GetComponentsInChildren<MonoBehaviour>(true);

            foreach (var mono in monos)
            {
                if (mono == null) continue;
                Injector.Inject(mono);
            }
        }
    }
}