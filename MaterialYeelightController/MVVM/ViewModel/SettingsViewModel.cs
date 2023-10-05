using MaterialYeelightController.Core;
using MaterialYeelightController.ThemeManager;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal class SettingsViewModel : ObservableObject
    {

        private static string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe";
        private static string appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appName);
        private static string userStartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        private static string userStartupFolderAppPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), appName.Replace(".exe", ".url"));
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

        private void CreateShortcutToStartupFolder()
        {
            using (StreamWriter writer = new StreamWriter(userStartupFolderAppPath))
            {
                writer.WriteLine("[InternetShortcut]");
                writer.WriteLine("URL=file:///" + appPath);
                writer.WriteLine("IconIndex=0");
                string icon = appPath.Replace('\\', '/');
                writer.WriteLine("IconFile=" + icon);
            }
        }

        private void DeleteShortcutToStartupFolder()
        {
            var file = new FileInfo(userStartupFolderAppPath);
            if (file.Exists)
            {
                file.Delete();
            }
        }
        public bool StartWithWindows
        {
            get { return _startWithWindows; }
            set
            {
                if (_startWithWindows != value)
                {
                    if (value)
                    {
                        if (IsAppRunningOnStartup())
                        {
                            DeleteShortcutToStartupFolder();
                        }
                        CreateShortcutToStartupFolder();
                    }
                    else
                    {
                        if (IsAppRunningOnStartup())
                        {
                            DeleteShortcutToStartupFolder();
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


            var file = new FileInfo(userStartupFolderAppPath);
            if (file.Exists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private RelayCommand _resetCommand;

        public RelayCommand ResetCommand
        {
            get { return _resetCommand; }
            set { _resetCommand = value; }
        }

        public SettingsViewModel(IThemeController themeController)
        {
            ThemeController = themeController;
            _startWithWindows = IsAppRunningOnStartup();

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
