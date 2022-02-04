using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using YeelightController.Core;
using YeelightController.Extensions;

namespace YeelightController.ThemeManager
{
    internal class ThemeController : ObservableObject, IThemeController
    {
        private Color _primaryColor;
        private Color _secondaryColor;
        private bool _isDarkModeEnabled = true;

        public bool IsDarkModeEnabled
        {
            get { return _isDarkModeEnabled; }
            set
            {
                if (value != _isDarkModeEnabled)
                {
                    _isDarkModeEnabled = value;
                    ModifyBaseTheme(_isDarkModeEnabled);
                    OnPropertyChanged(nameof(IsDarkModeEnabled));
                    Properties.Settings.Default.IsDarkModeEnabled = value;
                }

            }
        }

        public Color PrimaryColor
        {
            get { return _primaryColor; }
            set
            {
                if (value != _primaryColor)
                {
                    _primaryColor = value;
                    ChangePrimaryColor(_primaryColor);
                    
                    OnPropertyChanged(nameof(PrimaryColor));
                    Properties.Settings.Default.PrimaryColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
                }

            }
        }


        public Color SecondaryColor
        {
            get { return _secondaryColor; }
            set
            {
                if (value != _secondaryColor)
                {
                    _secondaryColor = value;
                    ChangeSecondaryColor(_secondaryColor);                    
                    OnPropertyChanged(nameof(SecondaryColor));
                    Properties.Settings.Default.SecondaryColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
                }

            }
        }
        

        public Color? PrimaryForegroundColor
        {
            get
            {
                return PrimaryColor.GetIdealLabelColor();
            }            
        }

        public ThemeController()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            //_isDarkModeEnabled = theme.GetBaseTheme() == BaseTheme.Dark;
            //_primaryColor = theme.GetBaseTheme() == BaseTheme.Light ? theme.PrimaryLight.Color : theme.PrimaryDark.Color;
            //_secondaryColor = theme.GetBaseTheme() == BaseTheme.Light ? theme.SecondaryLight.Color : theme.SecondaryDark.Color;
            IsDarkModeEnabled = Properties.Settings.Default.IsDarkModeEnabled;
            var pmColor = Properties.Settings.Default.PrimaryColor;
            PrimaryColor = Color.FromArgb(pmColor.A,pmColor.R,pmColor.G,pmColor.B);
            var sColor = Properties.Settings.Default.SecondaryColor;
            SecondaryColor= Color.FromArgb(sColor.A,sColor.R,sColor.G,sColor.B);
        }
        private void ModifyBaseTheme(bool isDarkTheme)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            theme.SetBaseTheme(isDarkTheme ? Theme.Dark : Theme.Light);
            paletteHelper.SetTheme(theme);
        }

        private void ChangePrimaryColor(Color color)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetPrimaryColor(color);            
            paletteHelper.SetTheme(theme);
        }
        private void ChangeSecondaryColor(Color color)
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetSecondaryColor(color);
            paletteHelper.SetTheme(theme);
        }
    }
}
