using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;
using YeelightController.Helpers;
using YeelightController.MVVM.Model;

namespace YeelightController.Extensions
{
    internal static class SmartDeviceExtensions
    {
        internal static async Task<bool> ToggleDevicePowerAsync(this SmartDevice smartDevice)
        {
           return await YeelightFunctions.ToggleDevicePowerAsync(smartDevice);
        }

        internal static async Task<bool> SetNameAsync(this SmartDevice smartDevice, string name)
        {            
            return await YeelightFunctions.SetNameAsync(smartDevice, name);
        }
        internal static async Task<bool> SetColorAsync(this SmartDevice smartDevice, string colorHex)
        {
            return await YeelightFunctions.SetColorAsync(smartDevice, colorHex);
        }

        internal static async Task<bool> TurnOnAsync(this SmartDevice smartDevice)
        {
            return await YeelightFunctions.TurnOnAsync(smartDevice);
        }
        internal static async Task<bool> TurnOffAsync(this SmartDevice smartDevice)
        {
            return await YeelightFunctions.TurnOffAsync(smartDevice);
        }

        internal static async Task<bool> SetColorTemperatureAsync(this SmartDevice smartDevice, int temperature)
        {
            return await YeelightFunctions.SetColorTemperatureAsync(smartDevice, temperature);
        }

        internal static async Task<bool> SetBrightnessAsync(this SmartDevice smartDevice, int brightness)
        {
            return await YeelightFunctions.SetBrightnessAsync(smartDevice, brightness);
        }
    }
}
