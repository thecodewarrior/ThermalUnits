using System;
using HarmonyLib;
using Verse;

namespace ThermalUnits.Patch
{
    internal static class TemperatureFormatting
    {
        public static string Format(float temperature, string format, bool isDelta)
        {
            float fahrenheitOffset = isDelta ? 0 : 32;
            float kelvinOffset = isDelta ? 0 : 273.15f;
            string fahrenheit = (temperature * 1.8f + fahrenheitOffset).ToString(format) + "F";
            string celsius = temperature.ToString(format) + "C";
            
            switch (Prefs.TemperatureMode)
            {
                case TemperatureDisplayMode.Celsius:
                    return celsius + " [" + fahrenheit + "]";
                case TemperatureDisplayMode.Fahrenheit:
                    return fahrenheit + " [" + celsius + "]";
                case TemperatureDisplayMode.Kelvin:
                    return (temperature + kelvinOffset).ToString(format) + "K"
                        + " [" + celsius + "/" + fahrenheit + "]";
                default:
                    throw new InvalidOperationException();
            }
        }
    }
    
    [HarmonyPatch(typeof(GenText), "ToStringTemperature")]
    // ReSharper disable once InconsistentNaming
    internal static class ToStringTemperature_Patch
    {
        static bool Prefix(float celsiusTemp, string format, ref string __result)
        {
            __result = TemperatureFormatting.Format(celsiusTemp, format, false);
            return false;
        }
    }
    
    [HarmonyPatch(typeof(GenText), "ToStringTemperatureOffset")]
    // ReSharper disable once InconsistentNaming
    internal static class ToStringTemperatureOffset_Patch
    {
        static bool Prefix(float celsiusTemp, string format, ref string __result)
        {
            __result = TemperatureFormatting.Format(celsiusTemp, format, true);
            return false;
        }
    }
}
