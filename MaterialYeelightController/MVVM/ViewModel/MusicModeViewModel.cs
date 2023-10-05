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
