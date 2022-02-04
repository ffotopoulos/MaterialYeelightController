using Microsoft.Extensions.DependencyInjection;
using YeelightController.MVVM.ViewModel;
using YeelightController.ThemeManager;

namespace YeelightController.DependencyContainer
{
    internal static class ContainerConfig
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IBaseViewModel, BaseViewModel>();
            services.AddSingleton<IThemeController, ThemeController>();
            services.AddSingleton<App>();          
            return services;
        }
        public static ServiceProvider BuildServices()
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            ServiceProvider = serviceProvider;
            return ServiceProvider;
        }
    }
}
