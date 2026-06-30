using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SUG.Essentials
{
    /// <summary>
    /// Essentials 框架入口。
    /// 负责创建 VContainer 并注册所有模块。
    /// </summary>
    public sealed class EssentialsBootstrap : LifetimeScope
    {
        [Header("Essentials Settings")]
        [SerializeField]
        private EssentialsSettingsSO settings;

        protected override void Configure(IContainerBuilder builder)
        {
            // 整个框架配置
            builder.RegisterInstance(settings);

            // 注册所有模块
            EssentialsInstaller.Install(builder, settings);
        }
    }
}