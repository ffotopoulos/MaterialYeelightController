using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;
using YeelightAPI.Models;
using YeelightAPI.Models.ColorFlow;
using MaterialYeelightController.Core;
using MaterialYeelightController.Extensions;
using MaterialYeelightController.MVVM.Model;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal class BaseViewModel : ObservableObject, IBaseViewModel
    {
        private ObservableCollection<SmartDevice> _devices;
        private FluentFlow flow;
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

        public RelayCommand ExitAppCommand { get; set; }
        public RelayCommand TurnAllCommand { get; set; }
        public RelayCommand ToggleDevicePowerCommand { get; set; }

        public BaseViewModel()
        {
            InitCommands();           
        }
        internal void InitCommands()
        {
            ExitAppCommand = new RelayCommand(async (o) =>
            {
                try
                {
                    if (Properties.Settings.Default.TurnOffDevicesOnExit)
                    {
                        await TurnAllDevicesState("off");
                    }
                    Properties.Settings.Default.Save();
                }
                catch (Exception) { }
                finally
                {
                    Environment.Exit(0);
                }
            });
            TurnAllCommand = new RelayCommand(async (state) =>
            {
                await TurnAllDevicesState(state);
            }, _ =>
            {
                return (Devices?.Count > 0) && (Devices?.Any(x => x.APIDevice.SupportedOperations.Any(x => x == METHODS.SetPower)) ?? false);
            });

            ToggleDevicePowerCommand = new RelayCommand(async (hostName) =>
            {
                await ToggleDevice(hostName);
            }, _ =>
            {
                return SelectedSmartDevice?.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.Toggle) ?? false;
            });

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

        public async Task DiscoverDevicesAsync()
        {
            try
            {
                if (Devices != null) Devices.Clear();
                else Devices = new ObservableCollection<SmartDevice>();

                if (SelectedSmartDevice != null)
                    SelectedSmartDevice = null;

                DeviceLocator.UseAllAvailableMulticastAddresses = Properties.Settings.Default.UseAllAvailableMulticastAddresses;
                var devices = (await DeviceLocator.DiscoverAsync()).ToList();

                foreach (var device in devices)
                {
                    var smartDevice = new SmartDevice()
                    {
                        Id = device.Id,
                        HostName = device.Hostname,
                        Port = device.Port,
                        Name = device.Name.IsBase64String() ? device.Name.Base64Decode() : device.Name,                                                
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
                    if (int.TryParse(deviceBrightnessProp.ToString(), out int bt))
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
                    Devices.Add(smartDevice);
                }
            }
            catch (Exception)
            {

            }
        }
        event PropertyChangedEventHandler? PropertyChanged;
    }
}
