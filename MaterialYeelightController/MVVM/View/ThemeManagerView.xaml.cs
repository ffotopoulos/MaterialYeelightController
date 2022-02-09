using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialYeelightController.ThemeManager;

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
