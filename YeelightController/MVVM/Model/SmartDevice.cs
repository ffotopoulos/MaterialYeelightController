using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;
using YeelightController.Core;
using YeelightController.Extensions;

namespace YeelightController.MVVM.Model
{
    internal enum DeviceType
    {
        Bulb,
        LightStrip,
        Other
    }
    internal class SmartDevice : ObservableObject
    {
        private string? _name;
        private string? _color;
        private DeviceType _type;
        private string _hostName;
        private int _port;

        private bool _isOn;

        
        public Device APIDevice{ get; set; } //we need to have a reference to the original API's device 
                                             //because new Device() will throw a Socket exception when connecting multiple times
        public string Id { get; set; }
        public bool IsOn
        {
            get { return _isOn; }
            set
            {
                if (value != _isOn)
                {
                    _isOn = value;
                    OnPropertyChanged(nameof(IsOn));
                }

            }
        }

        public string? Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null && value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public string? Color //in hex for binding purposes
        {
            get
            {
                return _color;
            }
            set
            {
                if (value != null && value != _color)
                {
                    _color = value;
                    OnPropertyChanged(nameof(Color));
                }

            }
        }

        private int _temperature;

        public int Temperature
        {
            get { return _temperature; }
            set
            {
                if(value!= _temperature)
                {
                    _temperature = value;
                    OnPropertyChanged();
                }
                
            }
        }
        private int _brightness;
        public int Brightness
        {
            get { return _brightness; }
            set
            {
                if (value != _brightness)
                {
                    _brightness = value;
                    OnPropertyChanged();
                }

            }
        }

        public DeviceType Type
        {
            get
            {
                return _type;
            }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }

            }
        }

        public string HostName { get => _hostName; set => _hostName = value; }
        public int Port { get => _port; set => _port = value; }


    }
}
