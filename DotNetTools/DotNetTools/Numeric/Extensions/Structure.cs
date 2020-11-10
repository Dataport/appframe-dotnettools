using System;
using System.Globalization;

namespace Dataport.AppFrameDotNet.DotNetTools.Numeric.Extensions
{
    /// <summary>
    /// Stellt Methoden zur strukturellen Beschaffenheit von numerischen Werten zur Verfügung
    /// </summary>
    public static class Structure
    {
        /// <summary>
        /// Zählt die Anzahl der Ziffern innerhalb der Zahl.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigits(this short i)
        {
            return i.ToString().CountDigitsInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Ziffern innerhalb der Zahl.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigits(this int i)
        {
            return i.ToString().CountDigitsInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Ziffern innerhalb der Zahl.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigits(this long i)
        {
            return i.ToString().CountDigitsInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Ziffern innerhalb der Zahl, die vor dem Komma stehen.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigits(this float i)
        {
            return Math.Truncate(i).ToString(CultureInfo.InvariantCulture).CountDigitsInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Ziffern innerhalb der Zahl, die vor dem Komma stehen.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigits(this double i)
        {
            return Math.Truncate(i).ToString(CultureInfo.InvariantCulture).CountDigitsInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Ziffern innerhalb der Zahl, die vor dem Komma stehen.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigits(this decimal i)
        {
            return Math.Truncate(i).ToString(CultureInfo.InvariantCulture).CountDigitsInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Dezimalstellen innerhalb der Zahl.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigitsDecimal(this float i)
        {
            return i.ToString("R").CountDigitsDecimalInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Dezimalstellen innerhalb der Zahl.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigitsDecimal(this double i)
        {
            return i.ToString("R").CountDigitsDecimalInternal();
        }

        /// <summary>
        /// Zählt die Anzahl der Dezimalstellen innerhalb der Zahl.
        /// </summary>
        /// <param name="i">Die zu prüfende Zahl.</param>
        /// <returns>Die Anzahl der Ziffern.</returns>
        public static int CountDigitsDecimal(this decimal i)
        {
            // Das Vorgehen mit ToString("R") führt bei Decimal zu einer Format-Exception
            return BitConverter.GetBytes(decimal.GetBits(i)[3])[2];
        }

        private static int CountDigitsInternal(this string str)
        {
            var cleanedString = str.Replace("-", "");
            if (cleanedString == "0")
            {
                return 0;
            }
            return cleanedString.Length;
        }

        private static int CountDigitsDecimalInternal(this string str)
        {
            var position = str.IndexOf(',');
            if (position == -1)
            {
                return 0;
            }

            return str.Substring(position + 1).CountDigitsInternal();
        }
    }
}