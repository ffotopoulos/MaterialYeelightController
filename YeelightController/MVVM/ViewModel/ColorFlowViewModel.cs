using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightController.Core;
using YeelightController.Extensions;
using YeelightController.MVVM.Model;

namespace YeelightController.MVVM.ViewModel
{
    internal class ColorFlowViewModel : ObservableObject
    {
        public RelayCommand StartFlowingCommand { get; private set; }
        public RelayCommand StopFlowingCommand { get; private set; }

        public ColorFlowViewModel(IBaseViewModel baseViewModel)
        {
            BaseViewModel = baseViewModel;
            ColorFlowObject = baseViewModel.SelectedSmartDevice?.ColorFlow;
            baseViewModel.PropertyChanged += BaseViewModel_PropertyChanged;
            InitCommands();
        }

        private void InitCommands()
        {
            StartFlowingCommand = new RelayCommand(async (o) =>
            {
                await BaseViewModel.SelectedSmartDevice.StartFlowing();
            });

            StopFlowingCommand = new RelayCommand(async (o) =>
            {
                await BaseViewModel.SelectedSmartDevice.StopFlowing();
            });
        }

        private void BaseViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ColorFlowObject = BaseViewModel.SelectedSmartDevice?.ColorFlow;
        }

        public IBaseViewModel BaseViewModel { get; }

        private ColorFlowModel _colorFlowObject;
        public ColorFlowModel ColorFlowObject
        {
            get
            {
                return _colorFlowObject;
            }
            set
            {
                if (_colorFlowObject != value)
                {
                    _colorFlowObject = value;
                    OnPropertyChanged(nameof(ColorFlowObject));
                }
            }
        }


    }
}
