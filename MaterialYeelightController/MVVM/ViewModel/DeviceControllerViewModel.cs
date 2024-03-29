﻿using MaterialYeelightController.Core;
using MaterialYeelightController.Extensions;
using MaterialYeelightController.ThemeManager;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal class DeviceControllerViewModel : ObservableObject
    {
        private RelayCommand _changeDeviceColorCommand;
        public RelayCommand ChangeDeviceColorCommand
        {
            get
            {
                return _changeDeviceColorCommand;
            }
            set
            {
                _changeDeviceColorCommand = value;
            }
        }
        private bool _isSavingName = false;

        public bool IsSavingName
        {
            get { return _isSavingName; }
            set
            {
                _isSavingName = value;
                OnPropertyChanged(nameof(IsSavingName));
            }
        }
        private bool _savingNameCompleted = false;
        public bool SavingNameCompleted
        {
            get
            {
                return _savingNameCompleted;
            }
            set
            {
                _savingNameCompleted = value;
                OnPropertyChanged(nameof(SavingNameCompleted));
            }
        }
        private RelayCommand _changeDeviceNameCommand;
        public RelayCommand ChangeDeviceNameCommand
        {
            get
            {
                return _changeDeviceNameCommand;
            }
            set
            {
                _changeDeviceNameCommand = value;
            }
        }

        private RelayCommand _changeDeviceStateCommand;
        public RelayCommand ChangeDeviceStateCommand
        {
            get
            {
                return _changeDeviceStateCommand;
            }
            set
            {
                _changeDeviceStateCommand = value;
            }
        }

        private RelayCommand _changeBrightnessCommand;

        public RelayCommand ChangeBrightnessCommand
        {
            get { return _changeBrightnessCommand; }
            set { _changeBrightnessCommand = value; }
        }

        private RelayCommand _changeTempCommand;

        public RelayCommand ChangeTempCommand
        {
            get { return _changeTempCommand; }
            set { _changeTempCommand = value; }
        }

        public IBaseViewModel BaseViewModel { get; }
        public IThemeController ThemeController { get; }

        public DeviceControllerViewModel(IBaseViewModel baseViewModel, IThemeController themeController)
        {
            BaseViewModel = baseViewModel;
            ThemeController = themeController;
            _selectedDeviceName = baseViewModel.SelectedSmartDevice?.Name;
            SelectedColor = baseViewModel.SelectedSmartDevice?.Color;
            BaseViewModel.PropertyChanged += BaseViewModel_PropertyChanged;
            InitCommands();
        }

        private void InitCommands() //TO-DO Move these to BaseViewModel
        {
            ChangeDeviceColorCommand = new RelayCommand(async (hex) =>
            {
                await ChangeDeviceColor(hex);
            }, _ =>
             {
                 return BaseViewModel != null && BaseViewModel.SelectedSmartDevice != null
                 && BaseViewModel.SelectedSmartDevice.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.SetRGBColor);
             });

            ChangeDeviceNameCommand = new RelayCommand(async (name) =>
            {
                await ChangeDeviceName(name);
            }, (name) =>
            {
                var canExec = name != null && !string.IsNullOrEmpty(name.ToString().Trim())
                    && SelectedDeviceName != null
                    && SelectedDeviceName.ToString() != name.ToString().Trim()
                    && BaseViewModel.SelectedSmartDevice.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.SetName);
                return canExec;
            });
            ChangeDeviceStateCommand = new RelayCommand(async (state) =>
            {
                await ChangeDeviceState(state);
            });

            ChangeBrightnessCommand = new RelayCommand(async (bt) =>
            {
                await BaseViewModel.SelectedSmartDevice.SetBrightnessAsync(int.Parse(bt.ToString()));
            }, _ =>
            {
                return BaseViewModel?.SelectedSmartDevice?.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.SetBrightness) ?? false;
            });

            ChangeTempCommand = new RelayCommand(async (ct) =>
            {
                await BaseViewModel.SelectedSmartDevice.SetColorTemperatureAsync(int.Parse(ct.ToString()));
            }, _ =>
            {
                return BaseViewModel?.SelectedSmartDevice?.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.SetColorTemperature) ?? false;
            }); ;
        }

        private async Task ChangeDeviceState(object state)
        {
            switch (state?.ToString())
            {
                case "on":
                    await BaseViewModel.SelectedSmartDevice.TurnOnAsync();
                    break;
                case "off":
                    await BaseViewModel.SelectedSmartDevice.TurnOffAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task ChangeDeviceColor(object hex)
        {
            if (hex != null)
            {
                BaseViewModel.SelectedSmartDevice.SetColorAsync(hex.ToString());
            }
        }
        private async Task ChangeDeviceName(object name)
        {
            if (!string.IsNullOrEmpty(name.ToString().Trim()))
            {
                IsSavingName = true;
                SavingNameCompleted = false;
                var base64name = name.ToString().Base64Encode();
                var success = await BaseViewModel.SelectedSmartDevice.SetNameAsync(base64name);
                if (success)
                    SelectedDeviceName = name.ToString();
                IsSavingName = false;
                SavingNameCompleted = true;
            }
        }

        private void BaseViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseViewModel.SelectedSmartDevice) && BaseViewModel.SelectedSmartDevice != null)
            {
                SelectedColor = BaseViewModel.SelectedSmartDevice?.Color;
                SelectedDeviceName = BaseViewModel.SelectedSmartDevice?.Name;
            }
        }
        private string? _selectedDeviceName;

        public string? SelectedDeviceName
        {
            get { return _selectedDeviceName; }
            set
            {
                if (_selectedDeviceName == null || value.Trim() != _selectedDeviceName.Trim())
                {
                    _selectedDeviceName = value;
                    OnPropertyChanged(nameof(SelectedDeviceName));
                }
            }
        }

        private string? _color;
        public string? SelectedColor
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
                    OnPropertyChanged(nameof(SelectedColor));
                }
            }
        }

    }
}
