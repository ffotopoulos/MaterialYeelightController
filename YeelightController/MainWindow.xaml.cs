using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using YeelightController.Helpers;

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
            //WindowState = WindowState.Maximized;
            if (Properties.Settings.Default.StartMinimised)
            {
                WindowState = WindowState.Minimized;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

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
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnMinimise_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnDonate_Click(object sender, RoutedEventArgs e)
         => Link.OpenInBrowser("https://paypal.me/ffsp");

        private void btnGitHub_Click(object sender, RoutedEventArgs e)
         => Link.OpenInBrowser("https://github.com/ffotopoulos/MaterialYeelightController");

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                iconMaxMin.Kind = PackIconKind.WindowRestore;
            }
            else
            {
                WindowState = WindowState.Normal;
                iconMaxMin.Kind = PackIconKind.WindowMaximize;
            }

        }
    }
}