using System;
using Dataport.AppFrameDotNet.DotNetTools.Time.Model;

namespace Dataport.AppFrameDotNet.DotNetTools.Time.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für <see cref="DateTime"/> zur Verfügung
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Gibt den ersten des Monats zurück in dem sich das Datum befindet.
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <returns>Ein DateTime-Objekt mit dem ersten Tag des Monats (Mitternacht) des Inputs.</returns>
        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// Gibt den letzten des Monats zurück in dem sich das Datum befindet.
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <returns>Ein DateTime-Objekt mit dem letzten Tag des Monats (Mitternacht) des Inputs.</returns>
        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Gibt zum Datum den ersten des nächsten Monats zurück in dem sich das Datum befindet.
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <returns>Ein DateTime-Objekt mit dem ersten Tag des nächsten Monats (Mitternacht) des Inputs.</returns>
        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1).AddMonths(1);
        }

        /// <summary>
        /// Gibt ein DateTime zurück, bei dem im Vergleich zum eingehenden DateTime informationen weggeschnitten werden.
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <param name="part">Gibt den Teil im DateTime an, bis zu dem reduziert wird.</param>
        /// <returns>Ein reduziertes DateTime</returns>
        public static DateTime Truncate(this DateTime dt, DateTimePart part)
        {
            if (part == DateTimePart.Year)
            {
                return DateTime.MinValue;
            }

            return new DateTime
            (
                dt.Year,
                part < DateTimePart.Months ? dt.Month : 1,
                part < DateTimePart.Days ? dt.Day : 1,
                part < DateTimePart.Hours ? dt.Hour : 0,
                part < DateTimePart.Minutes ? dt.Minute : 0,
                part < DateTimePart.Seconds ? dt.Second : 0,
                part < DateTimePart.Milliseconds ? dt.Millisecond : 0
            );
        }

        /// <summary>
        /// Gibt das Datum mit der min. Uhrzeit zurück (0:00 Uhr).
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <returns>Ein DateTime-Objekt des Inputs mit der Zeit Mitternacht.</returns>
        public static DateTime BeginOfDay(this DateTime dt)
        {
            return dt.Date;
        }

        /// <summary>
        /// Gibt das Datum mit der max. Uhrzeit zurück (23:59:59.999).
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <returns>Ein DateTime-Objekt mit einer Millisekunde vor Mitternacht des nächsten Tages des Inputs.</returns>
        public static DateTime EndOfDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day).AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Setzt das Datum auf einen bestimmten Tag im Monat fest ohne die anderen Datumskomponenten
        /// zu ändern.
        /// </summary>
        /// <param name="dt">Das DateTime-Objekt das als Grundlage der Berechnung dient.</param>
        /// <param name="day">Der neue Tag der gesetzt werden soll.</param>
        /// <returns>Ein DateTime-Objekt des Inputs mit dem ersetzten Tag.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Tag nicht valide ist.</exception>
        public static DateTime SetDay(this DateTime dt, int day)
        {
            return new DateTime(dt.Year, dt.Month, day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
        }
    }
}