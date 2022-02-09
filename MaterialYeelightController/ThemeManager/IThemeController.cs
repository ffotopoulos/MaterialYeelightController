using System.Windows.Media;

namespace MaterialYeelightController.ThemeManager
{
    public interface IThemeController
    {
        bool IsDarkModeEnabled { get; set; }
        Color PrimaryColor { get; set; }
        Color SecondaryColor { get; set; }
    }
}