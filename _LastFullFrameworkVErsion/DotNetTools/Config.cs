using System;
using System.Configuration;

namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Statische Klasse die den Zugriff auf die app.config kapselt.
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Holt eine Einstellung aus den AppSettings und löst eine InvalidOperationException aus wenn diese nicht definiert ist.
        /// </summary>
        /// <param name="setting">Key der Einstellung.</param>
        /// <returns></returns>
        public static string GetAppSetting(string setting)
        {
            try
            {
                var value = ConfigurationManager.AppSettings[setting];
                if (value == null) throw new InvalidOperationException($"Einstellung '{setting}' fehlt in app.config (Sektion appSettings).");
                return value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Einstellung '{setting}' konnte nicht aus der app.config gelesen werden (Sektion appSettings).", ex);
            }
        }

        /// <summary>
        /// Holt eine Einstellung aus den AppSettings oder gibt einen Default zurück wenn diese nicht definiert ist.
        /// </summary>
        /// <param name="setting">Key der Einstellung.</param>
        /// <param name="defaultValue">Default-Wert, wenn die Einstellung nicht vorhanden ist.</param>
        /// <returns></returns>
        public static string GetAppSetting(string setting, string defaultValue)
        {
            try
            {
                var value = ConfigurationManager.AppSettings[setting];
                if (value == null) return defaultValue;
                return value;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Einstellung '{setting}' konnte nicht aus der app.config gelesen werden (Sektion appSettings).", ex);
            }
        }

        /// <summary>
        /// Holt eine Einstellung aus den AppSettings und löst eine InvalidOperationException aus wenn diese 
        /// nicht definiert ist oder nicht in den Zieltyp konvertierbar ist.
        /// </summary>
        /// <param name="setting">Key der Einstellung.</param>
        /// <typeparam name="T">Erwarteter Datentyp.</typeparam>
        /// <returns></returns>
        public static T GetAppSetting<T>(string setting)
        {
            var value = Config.GetAppSetting(setting, null);

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Einstellung '{setting}' konnte nicht in den erwarteten Typ '{typeof(T)}' umgewandelt werden.", ex);
            }
        }

        /// <summary>
        /// Holt eine Einstellung aus den AppSettings und löst eine InvalidOperationException aus wenn diese 
        /// nicht in den Zieltyp konvertierbar ist.
        /// Fehlt das Setting oder der Value ist leer, wird der defaultValue ausgegeben.
        /// </summary>
        /// <param name="setting">Key der Einstellung.</param>
        /// <param name="defaultValue">Default-Wert, wenn die Einstellung nicht vorhanden ist.</param>
        /// <typeparam name="T">Erwarteter Datentyp.</typeparam>
        /// <returns></returns>
        public static T GetAppSetting<T>(string setting, T defaultValue)
        {
            var value = Config.GetAppSetting(setting, null);

            if (string.IsNullOrWhiteSpace(value)) return defaultValue;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Einstellung '{setting}' konnte nicht in den erwarteten Typ '{typeof(T)}' umgewandelt werden.", ex);
            }
        }
    }
}

