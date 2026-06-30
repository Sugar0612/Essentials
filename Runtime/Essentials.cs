using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static UnityEditor.ObjectChangeEventStream;

namespace SUG.Essentials
{
    public static class Essentials
    {
        public static IObjectResolver Resolver { get; private set; }
        public static EssentialsSettingsSO Settings { get; private set; }

        internal static void Initialize()
        {
            if (Resolver != null) return;

            // 组件Installer注册
            var builder = new ContainerBuilder();
            RuntimeBridge.Initialize();

            UIInitializaction(builder);

            Resolver = builder.Build();
        }

        private static void UIInitializaction(ContainerBuilder builder)
        {
            // 找到这个项目中唯一的配置文件
            Settings = Resources.Load<EssentialsSettingsSO>("Essentials/Bootstrap");
            if (Settings == null) { Debug.LogError("[Essentials] Settings not found in Resources!"); return; }
            UIInstaller.Install(builder, Settings.uiSetting);
        }
    }
}