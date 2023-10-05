using MaterialYeelightController.Core;
using MaterialYeelightController.Extensions;
using MaterialYeelightController.MVVM.Model;
using System.Linq;

namespace MaterialYeelightController.MVVM.ViewModel
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
            }, _ =>
            {
                return BaseViewModel != null && BaseViewModel.SelectedSmartDevice != null
                  && BaseViewModel.SelectedSmartDevice.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.StartColorFlow);
            });

            StopFlowingCommand = new RelayCommand(async (o) =>
            {
                await BaseViewModel.SelectedSmartDevice.StopFlowing();
            }, _ =>
            {
                return BaseViewModel != null && BaseViewModel.SelectedSmartDevice != null
                  && BaseViewModel.SelectedSmartDevice.APIDevice.SupportedOperations.Any(x => x == YeelightAPI.Models.METHODS.StopColorFlow);
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
