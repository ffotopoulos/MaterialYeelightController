using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightController.Core;
using YeelightController.Extensions;
using YeelightController.MVVM.Model;

namespace YeelightController.MVVM.ViewModel
{
    internal class BaseViewModel : ObservableObject, IBaseViewModel
    {
        private ObservableCollection<SmartDevice> _devices;
        public ObservableCollection<SmartDevice> Devices
        {
            get { return _devices; }
            set { _devices = value; }
        }  

        private SmartDevice _selectedSmartDevice;
        public SmartDevice SelectedSmartDevice
        {
            get { return _selectedSmartDevice; }
            set
            {
                if (_selectedSmartDevice == null || !_selectedSmartDevice.Equals(value))
                {
                    _selectedSmartDevice = value;
                    OnPropertyChanged(nameof(SelectedSmartDevice));
                }

            }
        }
        public async Task TurnAllDevicesState(object state)
        {
            if (state.ToString() == "on")
            {
                var tasks = _devices.Select(d =>
                {
                    return d.TurnOnAsync();
                });
                await Task.WhenAll(tasks);
            }
            else if (state.ToString() == "off")
            {
                var tasks = _devices.Select(d =>
                {
                    return d.TurnOffAsync();
                });
                await Task.WhenAll(tasks);
            }
        }
        public async Task<bool> ToggleDevice(object deviceHostName)
        {
            var device = _devices.SingleOrDefault(x => x.HostName == deviceHostName.ToString());
            if (device != null)
            {
                return await device.ToggleDevicePowerAsync();
            }
            return false;
        }


        event PropertyChangedEventHandler? PropertyChanged;
    }
}
