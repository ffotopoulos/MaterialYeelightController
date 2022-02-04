using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using YeelightAPI;
using YeelightController.Core;
using YeelightController.Extensions;
using YeelightController.Helpers;
using YeelightController.MVVM.Model;
using YeelightController.ThemeManager;

namespace YeelightController.MVVM.ViewModel
{
    internal class DevicesViewModel : ObservableObject
    {
        private ObservableCollection<SmartDevice> _devices;
        public ObservableCollection<SmartDevice> Devices => _devices;

        private RelayCommand _refreshDevicesCommand;

        public RelayCommand RefreshDevicesCommand
        {
            get
            {
                return _refreshDevicesCommand;
            }
            set
            {
                _refreshDevicesCommand = value;
            }
        }
        private RelayCommand _toggleDevicePowerCommand;
        public RelayCommand ToggleDevicePowerCommand
        {
            get
            {
                return _toggleDevicePowerCommand;
            }
            set
            {
                _toggleDevicePowerCommand = value;
            }
        }

        //private RelayCommand _selectDeviceCommand;

        //public RelayCommand SelectDeviceCommand
        //{
        //    get { return _selectDeviceCommand; }
        //    set { _selectDeviceCommand = value; }
        //}
        public RelayCommand TurnAllCommand { get; set; }
        public IBaseViewModel BaseViewModel { get; }
        public IThemeController ThemeController { get; }

        private bool _isLoading = true;        
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
        private bool _showUIButtons;
        public bool ShowUIButtons
        {
            get { return _showUIButtons; }
            set
            {
                _showUIButtons = value;
                OnPropertyChanged(nameof(ShowUIButtons));
            }
        }

        public DevicesViewModel(IBaseViewModel baseViewModel, IThemeController themeController)
        {
            _devices = new ObservableCollection<SmartDevice>();
            BaseViewModel = baseViewModel;
            ThemeController = themeController;
            CvsDevices = new CollectionViewSource();
            CvsDevices.Filter += ApplyFilter;
            InitCommands();
        }
        private void InitCommands()
        {
            RefreshDevicesCommand = new RelayCommand(async (o) => { await DiscoverDevicesAsync(); }
            , (o) =>
            {
                return !IsLoading;
            });
            ToggleDevicePowerCommand = new RelayCommand(async (hostName) => { await ToggleDevice(hostName); });
            //SelectDeviceCommand = new RelayCommand(async (hostName) => { await SelectDevice(hostName); });
            TurnAllCommand = new RelayCommand(async (state) => { await TurnAllDevicesState(state); }, (o) =>
            {
                return !IsLoading;
            });
        }
        internal CollectionViewSource CvsDevices { get; set; }
        public ICollectionView AllDevices
        {
            get { return CvsDevices.View; }
        }
        private string filter ;

        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
                OnPropertyChanged(nameof(Filter));
                OnFilterChanged();
            }
        }
        private void OnFilterChanged()
        {
            CvsDevices.View.Refresh();
        }

        void ApplyFilter(object sender, FilterEventArgs e)
        {
            SmartDevice device = (SmartDevice)e.Item;

            if (string.IsNullOrWhiteSpace(this.Filter) || this.Filter.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = device.Name.ToUpper().Contains(Filter.ToUpper());
            }
        }

        private async Task TurnAllDevicesState(object state)
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
        private async Task ToggleDevice(object deviceHostName)
        {
            var device = _devices.SingleOrDefault(x => x.HostName == deviceHostName.ToString());
            if (device != null)
            {
                await device.ToggleDevicePowerAsync();
            }
        }

        //private async Task SelectDevice(object deviceHostName)
        //{
        //    await Task.Run(() =>
        //    {
        //        //BaseViewModel.SelectedSmartDevice = null;
        //        var device = _devices.SingleOrDefault(x => x.HostName == deviceHostName.ToString());
        //        if (device != null)
        //        {
        //            if (BaseViewModel.SelectedSmartDevice != device)
        //                BaseViewModel.SelectedSmartDevice = device;
        //        }
        //    });
        //}
        public async Task DiscoverDevicesAsync(bool useAllAvailableMulticastAddresses = true)
        {
            try
            {
                IsLoading = true;
                if (_devices != null)
                    _devices.Clear();
                else
                    _devices = new ObservableCollection<SmartDevice>();
                if (BaseViewModel.SelectedSmartDevice != null)
                    BaseViewModel.SelectedSmartDevice = null;

                DeviceLocator.UseAllAvailableMulticastAddresses = useAllAvailableMulticastAddresses;
                var devices = (await DeviceLocator.DiscoverAsync()).ToList();

                foreach (var device in devices)
                {
                    var smartDevice = new SmartDevice()
                    {
                        Id = device.Id,
                        HostName = device.Hostname,
                        Port = device.Port,
                        Name = device.Name.IsBase64String() ? device.Name.Base64Decode() : device.Name
                    };

                    var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                    var deviceRGBProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.rgb.ToString()).Value;
                    if (deviceIsOnProp != null)
                        smartDevice.IsOn = deviceIsOnProp.ToString() == "on" ? true : false;
                    else
                        smartDevice.IsOn = false;

                    if (int.TryParse(deviceRGBProp.ToString(), out int value))
                    {
                        Color colorRgb = Color.FromArgb(value);
                        Color colorArgb = Color.FromArgb(255, colorRgb.R, colorRgb.G, colorRgb.B);
                        string hex = colorArgb.R.ToString("X2") + colorArgb.G.ToString("X2") + colorArgb.B.ToString("X2");
                        smartDevice.Color = "#" + hex;
                    }

                    var deviceBrightnessProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.bright.ToString()).Value;
                    if(int.TryParse(deviceBrightnessProp.ToString(),out int bt))
                    {
                        smartDevice.Brightness = bt;
                    }
                    var deviceCTProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.ct.ToString()).Value;
                    if (int.TryParse(deviceCTProp.ToString(), out int ct))
                    {
                        smartDevice.Temperature = ct;
                    }

                    if (device.Model == YeelightAPI.Models.MODEL.Color)
                        smartDevice.Type = DeviceType.Bulb;
                    else if (device.Model == YeelightAPI.Models.MODEL.Stripe)
                        smartDevice.Type = DeviceType.LightStrip;
                    else
                        smartDevice.Type = DeviceType.Other;

                    smartDevice.APIDevice = device; 
                    _devices.Add(smartDevice);
                }
                if (devices.Count > 0)
                {
                    BaseViewModel.SelectedSmartDevice = _devices[0];
                    CvsDevices.Source = this._devices;                    
                }
               
            }
            catch (Exception)
            {

            }
            finally
            {
                IsLoading = false;
            }

        }
    }
}
