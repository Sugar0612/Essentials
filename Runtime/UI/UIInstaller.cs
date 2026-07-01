using VContainer;
using VContainer.Unity;

namespace SUG.Essentials
{
    public static class UIInstaller
    {
        public static void Install(IContainerBuilder builder, UISettingsSO settings)
        {
            builder.RegisterInstance(settings);
            builder.Register<IUIService, UIManager>(Lifetime.Singleton);
        }
    }
}