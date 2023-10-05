using MaterialYeelightController.ThemeManager;
using System.Windows.Controls;

namespace MaterialYeelightController.MVVM.View
{
    /// <summary>
    /// Interaction logic for ThemeManager.xaml
    /// </summary>
    public partial class ThemeManagerView : UserControl
    {
        public IThemeController ThemeController { get; }
        public ThemeManagerView(IThemeController themeController)
        {
            InitializeComponent();
            ThemeController = themeController;
        }

    }
}
