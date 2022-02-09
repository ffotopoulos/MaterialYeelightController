using MaterialDesignThemes.Wpf;
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
using MaterialYeelightController.Core;
using MaterialYeelightController.Extensions;
using MaterialYeelightController.Helpers;
using MaterialYeelightController.MVVM.Model;
using MaterialYeelightController.ThemeManager;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal class DevicesViewModel : ObservableObject
    {

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

        public RelayCommand ShowDialogCommand { get; set; }

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
            InitCommands();
            BaseViewModel = baseViewModel;
            BaseViewModel.Devices = new ObservableCollection<SmartDevice>();

            ThemeController = themeController;
            CvsDevices = new CollectionViewSource();
            CvsDevices.Filter += ApplyFilter;

        }
        private void InitCommands()
        {
            RefreshDevicesCommand = new RelayCommand(async (o) =>
            {
                await DiscoverDevicesAsync();
            });
            ToggleDevicePowerCommand = new RelayCommand(async (hostName) => { await BaseViewModel.ToggleDevice(hostName); });
                     
        }
        internal CollectionViewSource CvsDevices { get; set; }
        public ICollectionView AllDevices
        {
            get { return CvsDevices.View; }
        }
        private string filter;

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
        public async Task DiscoverDevicesAsync()
        {
            try
            {
                IsLoading = true;

                await BaseViewModel.DiscoverDevicesAsync();
                if (BaseViewModel.Devices.Count > 0)
                {
                    BaseViewModel.SelectedSmartDevice = BaseViewModel.Devices[0];
                    CvsDevices.Source = this.BaseViewModel.Devices;
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


    }
}
