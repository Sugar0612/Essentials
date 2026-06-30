using System;
using System.Reflection;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Diagnostics;

namespace SUG.Essentials
{
    public class UISceneInitializer : Singleton<UISceneInitializer, SingletonGlobal>
    {
        //private MethodInfo _openUIMethod;

        //// =======================
        //// Life cycle
        //// =======================
        //private void Awake()
        //{
        //    _openUIMethod = typeof(IUIService).GetMethod("OpenUI", Type.EmptyTypes);
        //    SceneManager.sceneLoaded += OnSceneLoad;
        //}

        //private void OnSceneLoad(Scene sc, LoadSceneMode mode)
        //{
        //    IUIService uiMgr;
        //    if (ConfigManager.Get().HasConfig<SceneLocalConfig>())
        //    {
        //        SceneLocalConfig c = ConfigManager.Get().GetConfig<SceneLocalConfig>();
        //        foreach (var p in c.AutoOpenUITypes)
        //        {
        //            Type t = p.GetType();
        //            var genericOpen = _openUIMethod.MakeGenericMethod(t);
        //            genericOpen?.Invoke(uiMgr, null);
        //        }
        //    }
        //}
    }
}