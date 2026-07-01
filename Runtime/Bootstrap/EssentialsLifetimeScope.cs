using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace SUG.Essentials
{
    [DefaultExecutionOrder(-4000)]
    public sealed class EssentialsLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // 注册你的Install
            UIInstaller.Install(builder, Essentials.Settings.uiSetting);

            //AudioInstall.Install(builder);

            //SaveInstall.Install(builder);
        }

        protected override void Awake()
        {
            base.Awake();

            Essentials.SetContainer(Container);
        }
    }
}