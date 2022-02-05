using Microsoft.Extensions.DependencyInjection;
using System;
using YeelightController.Core;
using YeelightController.DependencyContainer;
using YeelightController.MVVM.View;
using YeelightController.ThemeManager;

namespace YeelightController.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private DevicesView _devicesView;
        private DeviceControllerView _deviceControllerView;



        public DeviceControllerView DeviceControllerView
        {
            get { return _deviceControllerView; }
            set { _deviceControllerView = value; }
        }


        public DevicesView DevicesView
        {
            get { return _devicesView; }
            private set { _devicesView = value; }
        }

        public IBaseViewModel BaseViewModel { get; private set; }
        public IThemeController ThemeController { get; private set; }

        private RelayCommand _exitAppCommand;

        public SettingsView SettingsView { get; private set; }

        public RelayCommand ExitAppCommand
        {
            get { return _exitAppCommand; }
            set { _exitAppCommand = value; }
        }


        public MainViewModel()
        {
            BaseViewModel = ContainerConfig.ServiceProvider.GetService<IBaseViewModel>();
            ThemeController = ContainerConfig.ServiceProvider.GetService<IThemeController>();
            DevicesView = new DevicesView();
            DevicesView.Loaded += DevicesView_Loaded;

            DeviceControllerView = new DeviceControllerView();
            DeviceControllerView.Loaded += DeviceControllerView_Loaded;

            SettingsView = new SettingsView();
            var sVM = new SettingsViewModel(ThemeController);
            SettingsView.DataContext = sVM;

            ExitAppCommand = new RelayCommand(async (o) =>
            {
                
               try
                {
                    if (sVM.TurnOffDevicesOnExit)
                    {
                        await BaseViewModel.TurnAllDevicesState("off");
                    }                  
                }
                catch (Exception) { }
                finally
                {
                    Environment.Exit(0);
                }
            });

        }

        private void SettingsView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private async void DeviceControllerView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var dcVM = new DeviceControllerViewModel(BaseViewModel, ThemeController);
            DeviceControllerView.DataContext = dcVM;
        }

        private async void DevicesView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var dVM = new DevicesViewModel(BaseViewModel, ThemeController);
            try
            {
                await dVM.DiscoverDevicesAsync();

                if (Properties.Settings.Default.TurnOnDevicesOnStartup)
                {
                    BaseViewModel.TurnAllDevicesState("on");
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                DevicesView.DataContext = dVM;
            }

        }


    }
}
