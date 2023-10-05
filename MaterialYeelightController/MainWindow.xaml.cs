using MaterialDesignThemes.Wpf;
using MaterialYeelightController.Helpers;
using System.Windows;
using System.Windows.Input;

namespace MaterialYeelightController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.StartMinimised)
            {
                WindowState = WindowState.Minimized;
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

        }

        private void draggablePanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
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
            Minimize();
        }

        private void Minimize()
        {
            this.Hide();
        }

        private void Maximize()
        {
            WindowState = WindowState.Maximized;
            iconMaxMin.Kind = PackIconKind.WindowRestore;
        }

        private void NormalWindowState()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            this.Show();
            iconMaxMin.Kind = PackIconKind.WindowMaximize;
        }

        private void btnDonate_Click(object sender, RoutedEventArgs e)
         => Link.OpenInBrowser("https://paypal.me/ffsp");

        private void btnGitHub_Click(object sender, RoutedEventArgs e)
         => Link.OpenInBrowser("https://github.com/ffotopoulos/MaterialYeelightController");

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                Maximize();
            }
            else
            {
                NormalWindowState();
            }
        }

        private void trayIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            NormalWindowState();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        => Link.OpenInBrowser("https://paypal.me/ffsp");
    }
}