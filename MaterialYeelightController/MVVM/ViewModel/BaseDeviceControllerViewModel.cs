using MaterialYeelightController.MVVM.View;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal class BaseDeviceControllerViewModel
    {
        public BaseDeviceControllerViewModel(IBaseViewModel baseViewModel, DeviceControllerView deviceControllerView, ColorFlowView colorFlowView)
        {
            BaseViewModel = baseViewModel;
            DeviceControllerView = deviceControllerView;
            ColorFlowView = colorFlowView;
        }

        public IBaseViewModel BaseViewModel { get; }
        public DeviceControllerView DeviceControllerView { get; }
        public ColorFlowView ColorFlowView { get; }
    }
}
