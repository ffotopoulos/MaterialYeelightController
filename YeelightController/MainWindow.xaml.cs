
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

namespace YeelightController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        } 

       
        private void draggablePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Begin dragging the window
            this.DragMove();
        }

        private void closeIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void ContentControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
