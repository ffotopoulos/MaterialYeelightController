using System.ComponentModel;
using YeelightController.MVVM.Model;

namespace YeelightController.MVVM.ViewModel
{
    internal interface IBaseViewModel
    {
        SmartDevice SelectedSmartDevice { get; set; }

        event PropertyChangedEventHandler? PropertyChanged;
    }
}