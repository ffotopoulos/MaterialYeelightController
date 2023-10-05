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
        public App()
        {
            ContainerConfig.BuildServices();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            using (var serviceProvider = ContainerConfig.ServiceProvider)
            {
                serviceProvider.GetService<App>().Run();
            }
        }
    }
}
