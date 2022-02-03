using System.Windows;
using System.Windows.Controls;

namespace YeelightController.MVVM.View
{
    /// <summary>
    /// Interaction logic for DeviceControllerView.xaml
    /// </summary>
    public partial class DeviceControllerView : UserControl
    {
        public DeviceControllerView()
        {
            InitializeComponent();            
        }

        private void sliderBrightness_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbBrigthnessPercentage.Text = $"{int.Parse(sliderBrightness.Value.ToString())}%";
        }

        private void sliderTemperature_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbTemperature.Text = $"{int.Parse(sliderTemperature.Value.ToString())}K";
        }
    }
}
