using System;

namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Allgemeine Helfer für Datumsangaben.
    /// </summary>
    /// <remarks></remarks>
    public static class DateExtensions
    {

        /// <summary>
        /// Gibt den ersten des Monats zurück in dem sich das Datum befindet.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime FirstDayOfMonth(this DateTime context)
        {
            return new DateTime(context.Year, context.Month, 1);
        }

        /// <summary>
        /// Gibt den letzten des Monats zurück in dem sich das Datum befindet.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime LastDayOfMonth(this DateTime context)
        {
            return new DateTime(context.Year, context.Month, 1).AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Gibt zum Datum den ersten des nächsten Monats zurück.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime FirstDayOfNextMonth(this DateTime context)
        {
            return new DateTime(context.Year, context.Month, 1).AddMonths(1);
        }

        /// <summary>
        /// Schneidet Datums/Zeitanteile ab:
        /// dateTime = dateTime.Truncate(TimeSpan.FromMilliseconds(1)); // Truncate to whole ms
        /// dateTime = dateTime.Truncate(TimeSpan.FromSeconds(1)); // Truncate to whole second
        /// dateTime = dateTime.Truncate(TimeSpan.FromMinutes(1)); // Truncate to whole minute
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }
   
        /// <summary>
        /// Gibt das Datum mit der min. Uhrzeit zurück (0:00 Uhr).
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime BeginOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        /// <summary>
        /// Gibt das Datum mit der max. Uhrzeit zurück (23:59:59.999).
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime EndOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(1).AddMilliseconds(-1);
        }

        /// <summary>
        /// Setzt das Datum auf einen bestimmten Tag im Monat fest ohne die anderen Datumskomponenten
        /// zu ändern.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime SetDay(this DateTime context, int day)
        {
            return new DateTime(context.Year, context.Month, day, context.Hour, context.Minute, context.Second, context.Millisecond);
        }

        /// <summary>
        /// Datum als Nullable. Für IF(), damit der richtige Type "Infered" wird.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DateTime? AsNullable(DateTime context)
        {
            return context;
        }
    }
}


