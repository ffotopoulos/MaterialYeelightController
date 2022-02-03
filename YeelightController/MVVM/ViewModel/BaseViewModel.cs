using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightController.Core;
using YeelightController.MVVM.Model;

namespace YeelightController.MVVM.ViewModel
{
    internal class BaseViewModel : ObservableObject, IBaseViewModel
    {
        private SmartDevice _selectedSmartDevice;
        public SmartDevice SelectedSmartDevice
        {
            get { return _selectedSmartDevice; }
            set
            {
                if (_selectedSmartDevice== null || !_selectedSmartDevice.Equals(value))
                {
                    _selectedSmartDevice = value;
                    OnPropertyChanged(nameof(SelectedSmartDevice));
                }

            }
        }
    }
}
