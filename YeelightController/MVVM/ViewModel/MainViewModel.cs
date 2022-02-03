
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightController.Core;
using YeelightController.DependencyContainer;
using YeelightController.Helpers;
using YeelightController.MVVM.View;

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

        private RelayCommand _exitAppCommand;

        public RelayCommand ExitAppCommand
        {
            get { return _exitAppCommand; }
            set { _exitAppCommand = value; }
        }


        public MainViewModel()
        {
            BaseViewModel = ContainerConfig.ServiceProvider.GetService<IBaseViewModel>();
            DevicesView = new DevicesView();
            DevicesView.Loaded += DevicesView_Loaded;

            DeviceControllerView = new DeviceControllerView();
            DeviceControllerView.Loaded += DeviceControllerView_Loaded;
            ExitAppCommand = new RelayCommand(o => { Environment.Exit(0); });
        }

        private async void DeviceControllerView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var dcVM = new DeviceControllerViewModel(BaseViewModel);
            DeviceControllerView.DataContext = dcVM;
        }

        private async void DevicesView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {            
            var dVM = new DevicesViewModel(BaseViewModel);
            await dVM.DiscoverDevicesAsync();
            DevicesView.DataContext = dVM;

        }
    }
}
