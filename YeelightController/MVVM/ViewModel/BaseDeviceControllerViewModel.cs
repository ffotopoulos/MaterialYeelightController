using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightController.MVVM.View;

namespace YeelightController.MVVM.ViewModel
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
