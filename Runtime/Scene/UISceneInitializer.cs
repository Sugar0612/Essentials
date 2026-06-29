using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Diagnostics;

namespace SUG.UnityCore
{
    public class UISceneInitializer : Singleton<UISceneInitializer, SingletonGlobal>
    {
        private MethodInfo _openUIMethod;

        // =======================
        // Life cycle
        // =======================
        private void Awake()
        {
            _openUIMethod = typeof(UIManager).GetMethod("OpenUI", Type.EmptyTypes);
            SceneManager.sceneLoaded += OnSceneLoad;
        }

        private void OnSceneLoad(Scene sc, LoadSceneMode mode)
        {
            UIManager uiMgr = UIManager.Get();
            if (ConfigManager.Get().HasConfig<SceneLocalConfig>())
            {
                SceneLocalConfig c = ConfigManager.Get().GetConfig<SceneLocalConfig>();
                foreach (var p in c.AutoOpenUITypes)
                {
                    Type t = p.GetType();
                    var genericOpen = _openUIMethod.MakeGenericMethod(t);
                    genericOpen?.Invoke(uiMgr, null);
                }
            }
        }
    }
}