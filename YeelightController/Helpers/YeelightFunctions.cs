using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YeelightAPI;
using YeelightController.Extensions;
using YeelightController.MVVM.Model;

namespace YeelightController.Helpers
{
    internal static class YeelightFunctions
    {
        internal static async Task<bool> ToggleDevicePowerAsync(SmartDevice smartDevice)
        {
           
            var success = false;
            try
            {
                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();
                success = await device.Toggle();
                await Task.Delay(1500); ;
                if (success)
                {
                    var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                    if (deviceIsOnProp != null)
                        smartDevice.IsOn = deviceIsOnProp.ToString() == "on" ? true : false;
                    else
                        smartDevice.IsOn = false;
                }
            }
            catch (Exception)
            {

            }
            return success;
        }
        internal static async Task<bool> TurnOnAsync(SmartDevice smartDevice)
        {
           
            var success = false;
            try
            {
                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();

                success = await device.SetPower(true, 1000);
                await Task.Delay(1500);
                var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                if (deviceIsOnProp != null)
                    smartDevice.IsOn = deviceIsOnProp.ToString() == "on" ? true : false;
                else
                    smartDevice.IsOn = false;
            }
            catch (Exception)
            {
            }
            return success;
        }

        internal static async Task<bool> TurnOffAsync(SmartDevice smartDevice)
        {
           

            var success = false;
            try
            {
                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();
                success = await device.SetPower(false, 1000);
                await Task.Delay(1500);
                var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                if (deviceIsOnProp != null)
                    smartDevice.IsOn = deviceIsOnProp.ToString() == "on" ? true : false;
                else
                    smartDevice.IsOn = false;
            }
            catch (Exception)
            {

            }
            return success;
        }

        internal static async Task<bool> SetNameAsync(SmartDevice smartDevice, string name)
        {
            var success = false;
            try
            {

                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();

                
                success = await device.SetName(name);
                await Task.Delay(1500); ;
                smartDevice.Name = device.Name.IsBase64String() ? device.Name.Base64Decode() : device.Name;
            }
            catch (Exception)
            {


            }
            return success;
        }
        internal static async Task<bool> SetColorAsync(SmartDevice smartDevice, string colorHex)
        {
            var success = false;
            var canExecute = true;
            try
            {
                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();
                var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                if (deviceIsOnProp.ToString() != "on")
                {
                    canExecute = await device.SetPower(true, 1000);
                    await Task.Delay(1500);
                    smartDevice.IsOn = canExecute;
                }
                if (canExecute)
                {
                    Color color = ColorTranslator.FromHtml(colorHex);
                    success = await device.SetRGBColor(color.R, color.G, color.B, 1000);
                    await Task.Delay(1500);
                    var deviceRGBProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.rgb.ToString()).Value;
                    if (int.TryParse(deviceRGBProp.ToString(), out int value))
                    {
                        Color colorRgb = Color.FromArgb(value);
                        Color colorArgb = Color.FromArgb(255, colorRgb.R, colorRgb.G, colorRgb.B);
                        string hex = colorArgb.R.ToString("X2") + colorArgb.G.ToString("X2") + colorArgb.B.ToString("X2");
                        smartDevice.Color = "#" + hex;
                    }
                }

            }
            catch (Exception)
            {
                return false;
            }

            return success;
        }

        internal static async Task<bool> SetBrightnessAsync(SmartDevice smartDevice, int brightness)
        {
            var success = false;
            var canExecute = true;
            try
            {
                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();

            
                var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                if (deviceIsOnProp.ToString() != "on")
                {
                    canExecute = await device.SetPower(true, 1000);
                    await Task.Delay(1500);
                    smartDevice.IsOn = canExecute;
                }
                if (canExecute)
                {
                    success = await device.SetBrightness(brightness, 1000);
                    await Task.Delay(1500);
                    if (success)
                    {
                        var deviceBrightnessProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.bright.ToString());
                        if (int.TryParse(deviceBrightnessProp.ToString(), out int bt))
                        {
                            smartDevice.Brightness = bt;
                        }
                    }
                }
                
            }
            catch (Exception)
            {

            }
            return success;
        }

        internal static async Task<bool> SetColorTemperatureAsync(SmartDevice smartDevice, int temperature)
        {
            var success = true;
            var canExecute = true;
            try
            {
                //var device = new Device(smartDevice.HostName, smartDevice.Port);
                var device = smartDevice.APIDevice;
                if (!device.IsConnected) await device.Connect();

              
                var deviceIsOnProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.power.ToString()).Value;
                if (deviceIsOnProp.ToString() != "on")
                {
                    canExecute = await device.SetPower(true, 1000);
                    await Task.Delay(1500);
                    smartDevice.IsOn = canExecute;
                }
                if (canExecute)
                {
                    success = await device.SetColorTemperature(temperature, 1000);
                    await Task.Delay(1000);
                    var deviceCTProp = device.Properties.FirstOrDefault(x => x.Key == YeelightAPI.Models.PROPERTIES.ct.ToString());
                    if (int.TryParse(deviceCTProp.ToString(), out int ct))
                    {
                        smartDevice.Temperature = ct;
                    }
                }                
            }
            catch (Exception)
            {
            }
            return success;
        }

    }
}
