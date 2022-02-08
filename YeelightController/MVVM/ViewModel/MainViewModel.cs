using MaterialDesignThemes.Wpf;
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
        public BaseDeviceControllerView BaseDeviceControllerView { get; private set; }
      
        public DevicesView DevicesView
        {
            get { return _devicesView; }
            private set { _devicesView = value; }
        }

        public IBaseViewModel BaseViewModel { get; private set; }
        public IThemeController ThemeController { get; private set; }

        private RelayCommand _exitAppCommand;

        public SettingsView SettingsView { get; private set; }
        public ThemeManagerView ThemeManagerView { get; private set; }

        public RelayCommand ExitAppCommand
        {
            get { return _exitAppCommand; }
            set { _exitAppCommand = value; }
        }

        public RelayCommand ShowDialogCommand { get; private set; }

        public MainViewModel()
        {
            InitMVVMContext();
            InitCommands();
        }

        private void InitCommands()
        {
            ExitAppCommand = new RelayCommand(async (o) =>
            {
                try
                {
                    if (Properties.Settings.Default.TurnOffDevicesOnExit)
                    {
                        await BaseViewModel.TurnAllDevicesState("off");
                    }
                    Properties.Settings.Default.Save();
                }
                catch (Exception) { }
                finally
                {
                    Environment.Exit(0);
                }
            });
            ShowDialogCommand = new RelayCommand(async (view) =>
            {
                if (view.ToString() == "settings")
                    await DialogHost.Show(SettingsView);
                else if (view.ToString() == "theme")
                    await DialogHost.Show(ThemeManagerView);

            });
        }
        private void InitMVVMContext()
        {
            BaseViewModel = ContainerConfig.ServiceProvider.GetService<IBaseViewModel>();
            ThemeController = ContainerConfig.ServiceProvider.GetService<IThemeController>();
            DevicesView = new DevicesView();
            DevicesView.Loaded += DevicesView_Loaded;

            var deviceControllerView = new DeviceControllerView();
            var dcVM = new DeviceControllerViewModel(BaseViewModel, ThemeController);
            deviceControllerView.DataContext = dcVM;

            var colorFlowView = new ColorFlowView();
            var cfVM = new ColorFlowViewModel(BaseViewModel);
            colorFlowView.DataContext = cfVM; 

            SettingsView = new SettingsView();
            var sVM = new SettingsViewModel(ThemeController);
            SettingsView.DataContext = sVM;

            ThemeManagerView = new ThemeManagerView(ThemeController);

            BaseDeviceControllerView = new BaseDeviceControllerView();
            var baseDeviceControllerViewModel = new BaseDeviceControllerViewModel(BaseViewModel, deviceControllerView,colorFlowView);
            BaseDeviceControllerView.DataContext = baseDeviceControllerViewModel;
            
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
