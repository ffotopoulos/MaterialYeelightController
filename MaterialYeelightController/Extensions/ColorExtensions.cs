using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialYeelightController.Extensions
{
    internal static class ColorExtensions
    {
        internal static Color GetIdealLabelColor(this Color c)
        {
            var brightness = (c.R * 0.299f + c.G * 0.587f + c.B * 0.114f) / 256f;
            var color =  brightness < 0.55 ? System.Drawing.Color.White : System.Drawing.Color.Black;
            return Color.FromArgb(color.A,color.R,color.G,color.B);
        }
    }
}
