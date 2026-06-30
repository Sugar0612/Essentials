using VContainer;

namespace SUG.Essentials
{
    /// <summary>
    /// Essentials 总安装器。
    /// 所有模块统一在这里注册。
    /// </summary>
    public static class EssentialsInstaller
    {
        public static void Install(
            IContainerBuilder builder,
            EssentialsSettingsSO settings)
        {
            UIInstaller.Install(builder, settings.uiSetting);

            // AudioInstaller.Install(builder, settings.Audio);
            // PoolInstaller.Install(builder, settings.Pool);
            // SaveInstaller.Install(builder, settings.Save);
        }
    }
}