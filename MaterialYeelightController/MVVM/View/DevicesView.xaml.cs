using MaterialYeelightController.Helpers;
using System.Windows;
using System.Windows.Controls;

namespace MaterialYeelightController.MVVM.View
{
    /// <summary>
    /// Interaction logic for DevicesView.xaml
    /// </summary>
    public partial class DevicesView : UserControl
    {
        public DevicesView()
        {
            InitializeComponent();
        }

        private void StackPanel_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void stackPanelProgressBar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var panel = sender as StackPanel;
            if (panel.Visibility == Visibility.Visible)
            {
                btnRefresh.IsEnabled = false;
                btnTurnOff.IsEnabled = false;
                btnTurnOn.IsEnabled = false;
            }
            else
            {
                btnRefresh.IsEnabled = true;
                btnTurnOn.IsEnabled = true;
                btnTurnOff.IsEnabled = true;
            }
        }


        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Link.OpenInBrowser(e.Uri.ToString());
            e.Handled = true;
        }

    }
}
