using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YeelightController.Core;

namespace YeelightController.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {
        private bool _useAllAvailableMulticastAddresses;
        private static string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe";
        private static string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appName);
        private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public bool UseAllAvailableMulticastAddresses
        {
            get { return _useAllAvailableMulticastAddresses; }
            set
            {
                if (_useAllAvailableMulticastAddresses != value)
                {
                    _useAllAvailableMulticastAddresses = value;
                    OnPropertyChanged(nameof(UseAllAvailableMulticastAddresses));
                    Properties.Settings.Default.UseAllAvailableMulticastAddresses = _useAllAvailableMulticastAddresses;
                }

            }
        }

        private bool _startWithWindows;


        public bool StartWithWindows
        {
            get { return _startWithWindows; }
            set
            {
                if (_startWithWindows != value)
                {
                    if (value)
                    {
                        if (!IsAppRunningOnStartup())
                        {
                            rkApp.SetValue(appName, appPath);
                        }
                    }
                    else
                    {
                        if (IsAppRunningOnStartup())
                        {
                            rkApp.DeleteValue(appName);
                        }
                    }
                    _startWithWindows = value;
                    OnPropertyChanged(nameof(StartWithWindows));
                }

            }
        }

        private bool _startMinimised;


        public bool StartMinimised
        {
            get { return _startMinimised; }
            set
            {
                if (_startMinimised != value)
                {

                    _startMinimised = value;
                    OnPropertyChanged(nameof(StartMinimised));
                    Properties.Settings.Default.StartMinimised = _startMinimised;
                }

            }
        }

        private bool _turnOnDevicesOnStartup;


        public bool TurnOnDevicesOnStartup
        {
            get { return _turnOnDevicesOnStartup; }
            set
            {
                if (_turnOnDevicesOnStartup != value)
                {

                    _turnOnDevicesOnStartup = value;
                    OnPropertyChanged(nameof(TurnOnDevicesOnStartup));
                    Properties.Settings.Default.TurnOnDevicesOnStartup = _turnOnDevicesOnStartup;
                }

            }
        }

        private bool _turnOffDevicesOnExit;


        public bool TurnOffDevicesOnExit
        {
            get { return _turnOffDevicesOnExit; }
            set
            {
                if (_turnOffDevicesOnExit != value)
                {

                    _turnOffDevicesOnExit = value;
                    OnPropertyChanged(nameof(TurnOffDevicesOnExit));
                    Properties.Settings.Default.TurnOffDevicesOnExit = _turnOffDevicesOnExit;
                }

            }
        }

        public bool IsAppRunningOnStartup()
        {
            return rkApp.GetValue(appName) != null;
        }
        public SettingsViewModel()
        {
            _useAllAvailableMulticastAddresses = Properties.Settings.Default.UseAllAvailableMulticastAddresses;
            _startWithWindows = IsAppRunningOnStartup();
            _startMinimised = Properties.Settings.Default.StartMinimised;
            _turnOnDevicesOnStartup = Properties.Settings.Default.TurnOnDevicesOnStartup;
            _turnOffDevicesOnExit = Properties.Settings.Default.TurnOffDevicesOnExit;
        }
    }
}
