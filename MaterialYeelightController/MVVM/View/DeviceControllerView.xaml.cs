using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MaterialYeelightController.MVVM.View
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

        private void tbHex_TextChanged(object sender, TextChangedEventArgs e)
        {
            var hex = tbHex.Text;
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(hex);
                colorPicker.Color = color;
            }
            catch (Exception ex)
            {

            }
        }

        private void tbRGB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentColor = colorPicker.Color;
            byte a = currentColor.A;
            byte r = currentColor.R;
            byte g = currentColor.G;
            byte b = currentColor.B;
            var tb = sender as TextBox;
            try
            {
                if (tb == tbR)
                {
                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        r = Convert.ToByte(tb.Text);
                    }
                }
                else if (tb == tbG)
                {
                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        g = Convert.ToByte(tb.Text);
                    }
                }
                else if (tb == tbB)
                {
                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        b = Convert.ToByte(tb.Text);
                    }
                }
                var finalColor = Color.FromArgb(a, r, g, b);
                colorPicker.Color = finalColor;
                string hex = finalColor.A.ToString("X2")+ finalColor.R.ToString("X2") + finalColor.G.ToString("X2") + finalColor.B.ToString("X2");
                tbHex.Text = "#" + hex;                
            }
            catch (Exception)
            {

            }
            
        }
    }
}
