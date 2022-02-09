using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using MaterialYeelightController.MVVM.Model;

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
    }
}