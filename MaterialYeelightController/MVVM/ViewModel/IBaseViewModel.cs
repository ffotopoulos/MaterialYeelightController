using MaterialYeelightController.Core;
using MaterialYeelightController.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal interface IBaseViewModel
    {
        ObservableCollection<SmartDevice> Devices { get; set; }
        SmartDevice SelectedSmartDevice { get; set; }
        event PropertyChangedEventHandler? PropertyChanged;
        Task<bool> ToggleDevice(object deviceHostName);
        Task DiscoverDevicesAsync();
        Task TurnAllDevicesState(object state);
        RelayCommand ExitAppCommand { get; set; }
        RelayCommand TurnAllCommand { get; set; }
        RelayCommand ToggleDevicePowerCommand { get; set; }

    }
}