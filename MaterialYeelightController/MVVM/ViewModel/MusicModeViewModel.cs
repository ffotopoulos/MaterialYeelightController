using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialYeelightController.MVVM.ViewModel
{
    internal class MusicModeViewModel
    {
        public MusicModeViewModel(IBaseViewModel baseViewModel)
        {
            BaseViewModel = baseViewModel;
        }

        public IBaseViewModel BaseViewModel { get; }
    }
}
