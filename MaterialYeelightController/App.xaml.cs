using MaterialYeelightController.DependencyContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace MaterialYeelightController
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            _serviceProvider = ContainerConfig.BuildServices();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            using (_serviceProvider)
            {
                _serviceProvider.GetService<App>()?.Run();
            }
        }
    }
}
