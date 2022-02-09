using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using MaterialYeelightController.Core;
using MaterialYeelightController.Extensions;
using MaterialYeelightController.MVVM.Model;

namespace MaterialYeelightController.ThemeManager
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
        private PaletteModel _selectedPalette;

        public PaletteModel SelectedPalette
        {
            get { return _selectedPalette; }
            set
            {
                if (value != _selectedPalette)
                {
                    _selectedPalette = value;
                    PrimaryColor = (Color)ColorConverter.ConvertFromString(value.PrimaryColor);
                    SecondaryColor = (Color)ColorConverter.ConvertFromString(value.SecondaryColor);
                    OnPropertyChanged(nameof(SelectedPalette));
                }
            }
        }

        public List<PaletteModel> DefaultPalettes { get; private set; }
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
                    Properties.Settings.Default.Save();
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
                    Properties.Settings.Default.Save();
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
            DefaultPalettes = new List<PaletteModel>();
            DefaultPalettes.AddRange(new PaletteModel[] {
                new PaletteModel{PrimaryColor = "#e5acff", SecondaryColor="#FF926889"},
                new PaletteModel{PrimaryColor = "#FFCA9B60", SecondaryColor="#FFB07F6E"},
                new PaletteModel{PrimaryColor = "#6EB257", SecondaryColor="#C5E063"},
                new PaletteModel{PrimaryColor = "#D1C6AD", SecondaryColor="#BBADA0"},
                new PaletteModel{PrimaryColor = "#FFADE5F9", SecondaryColor="#274C77"}
            });
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();          
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
