using VContainer;
using VContainer.Unity;

namespace SUG.Essentials
{
    public static class UIInstaller
    {
        public static void Install(IContainerBuilder builder, UISettingsSO settings)
        {
            builder.RegisterInstance(settings);
            builder.RegisterInstance(RuntimeBridge.UI);
            builder.Register<IUIService, UIManager>(Lifetime.Singleton);
        }
    }
}