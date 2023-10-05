using System.Windows;
using System.Windows.Controls;

namespace MaterialYeelightController.MVVM.View
{
    /// <summary>
    /// Interaction logic for ColorFlow.xaml
    /// </summary>
    public partial class ColorFlowView : UserControl
    {
        public ColorFlowView()
        {
            InitializeComponent();
        }



        private void speedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbSliderSpeed.Text = $"Flow Speed: {int.Parse(speedSlider.Value.ToString())}ms";
        }

        private void sleepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tbSleepTime.Text = $"Wait duration before changing color: {int.Parse(sleepSlider.Value.ToString()) / 1000}s";
        }
    }
}
