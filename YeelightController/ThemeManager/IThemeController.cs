using System.Windows.Media;

namespace YeelightController.ThemeManager
{
    internal interface IThemeController
    {
        bool IsDarkModeEnabled { get; set; }
        Color PrimaryColor { get; set; }
        Color SecondaryColor { get; set; }
    }
}