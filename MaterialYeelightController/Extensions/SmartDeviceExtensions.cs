using MaterialYeelightController.Helpers;
using MaterialYeelightController.MVVM.Model;
using System.Threading.Tasks;

namespace MaterialYeelightController.Extensions
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
        internal static async Task<bool> StartFlowing(this SmartDevice smartDevice)
        {
            return await YeelightFunctions.StartFlowing(smartDevice);
        }
        internal static async Task<bool> StopFlowing(this SmartDevice smartDevice)
        {
            return await YeelightFunctions.StopFlowing(smartDevice);
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
