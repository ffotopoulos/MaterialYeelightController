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
            tbSleepTime.Text = $"Wait duration before changing color: {int.Parse(sleepSlider.Value.ToString())/1000}s";
        }
    }
}
