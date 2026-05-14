
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

namespace SUG_UnityCore
{
    public interface ISingleton { }
    /// <summary>
    /// 全局单例标记：挂到 GLOBAL MANAGERS 下，跨场景不销毁
    /// </summary>
    public class SingletonLocal : ISingleton { }

    /// <summary>
    /// 本地单例标记：挂到 LOCAL MANAGERS 下，随场景销毁
    /// </summary>
    public class SingletonGlobal : ISingleton { }

    // ==============================
    // 全局父节点，挂在父节点下的子物体不会随着场景切换而销毁
    //===============================
    internal sealed class GlobalManagersRoot : MonoBehaviour
    {
        private static GlobalManagersRoot _instance;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// 通用单例基类，自动处理实例查找、自动创建、自动挂对应父节点
    /// </summary>
    /// <typeparam name="T"> 单例组件类型 </typeparam>
    /// <typeparam name="S"> 单例类型标记：SingletonGlobal / SingletonLocal </typeparam>
    public class Singleton<T, S> : MonoBehaviour where S : class, ISingleton where T : MonoBehaviour
    {
        private static T _instance;

        private readonly static Dictionary<Type, Transform> _parentCache = new();

        private static ILogger logger = new UnityLogger();

        private const string GLOBAL_MANAGERS = "GLOBAL MANAGERS";
        private const string LOCAL_MANAGERS  = "LOCAL MANAGERS";

        protected void OnDestroy()
        {
            if (_instance != null)
                Destroy(_instance);
        }

        public static T Get()
        {
            if (_instance != null) return _instance;

            var arr = GameObject.FindObjectsOfType<T>(true);
            if (arr.Length > 0)
            {
                if (arr.Length > 1) 
                    logger.LogError($"找到多个 {typeof(T).Name} 单例实例！只会保留第一个，其他的会被忽略。");

                _instance = arr[0];
                return _instance;
            }

            return CreateInstance();
        }

        private static T CreateInstance()
        {
            Transform p = TryGetInstanceParent();
            var go = new GameObject(typeof(T).Name);
            go.transform.SetParent(p);
            go.SetActive(true);

            _instance = go.AddComponent<T>();
            return _instance;
        }

        private static Transform TryGetInstanceParent()
        {
            var markerType = typeof(S);
            if (_parentCache.TryGetValue(markerType, out var v)) return v;
            string parName = markerType == typeof(SingletonGlobal) ? GLOBAL_MANAGERS : LOCAL_MANAGERS;
            var parGo = GameObject.Find(parName);
            if (parGo == null)
            {
                parGo = new GameObject(parName);
                if (markerType == typeof(SingletonGlobal)) parGo.AddComponent<GlobalManagersRoot>();
                _parentCache[markerType] = parGo.transform;
            }
            return parGo.transform;
        }
    }
}