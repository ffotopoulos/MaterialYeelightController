using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YeelightController.Core;
using YeelightController.ThemeManager;

namespace YeelightController.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {
        
        private static string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe";
        private static string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appName);
        private RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public bool UseAllAvailableMulticastAddresses
        {
            get { return Properties.Settings.Default.UseAllAvailableMulticastAddresses; }
            set
            {
                if (Properties.Settings.Default.UseAllAvailableMulticastAddresses != value)
                {
                    Properties.Settings.Default.UseAllAvailableMulticastAddresses = value;
                    OnPropertyChanged(nameof(UseAllAvailableMulticastAddresses));
                    SaveSettings();
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

       


        public bool StartMinimised
        {
            get { return Properties.Settings.Default.StartMinimised; }
            set
            {
                if (Properties.Settings.Default.StartMinimised != value)
                {

                    Properties.Settings.Default.StartMinimised = value;
                    OnPropertyChanged(nameof(StartMinimised));                    
                    SaveSettings();
                }

            }
        }

        


        public bool TurnOnDevicesOnStartup
        {
            get { return Properties.Settings.Default.TurnOnDevicesOnStartup; }
            set
            {
                if (Properties.Settings.Default.TurnOnDevicesOnStartup != value)
                {

                    Properties.Settings.Default.TurnOnDevicesOnStartup = value;
                    OnPropertyChanged(nameof(TurnOnDevicesOnStartup));
                    SaveSettings();
                }

            }
        }

        

        public bool TurnOffDevicesOnExit
        {
            get { return Properties.Settings.Default.TurnOffDevicesOnExit; }
            set
            {
                if (Properties.Settings.Default.TurnOffDevicesOnExit != value)
                {

                    Properties.Settings.Default.TurnOffDevicesOnExit = value;
                    OnPropertyChanged(nameof(TurnOffDevicesOnExit));
                    SaveSettings();
                }

            }
        }

        public IThemeController ThemeController { get; }

        public void SaveSettings()
        {
            Task.Run(() => { Properties.Settings.Default.Save(); });
        }
        public bool IsAppRunningOnStartup()
        {
            return rkApp.GetValue(appName) != null;
        }

        private RelayCommand _resetCommand;

        public RelayCommand ResetCommand
        {
            get { return _resetCommand; }
            set { _resetCommand = value; }
        }

        public SettingsViewModel(IThemeController themeController)
        {             
            _startWithWindows = IsAppRunningOnStartup();
            ThemeController = themeController;
            ResetCommand = new RelayCommand(o =>
            {
                ResetSettings();
            });
        }

        public void ResetSettings()
        {
            Properties.Settings.Default.Reset();
            StartWithWindows = Properties.Settings.Default.StartWithWindows;
            OnPropertyChanged(nameof(TurnOffDevicesOnExit));
            OnPropertyChanged(nameof(StartMinimised));
            OnPropertyChanged(nameof(StartWithWindows));
            OnPropertyChanged(nameof(TurnOnDevicesOnStartup));
            OnPropertyChanged(nameof(UseAllAvailableMulticastAddresses));
        }
    }
}
