using System;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für Enums zur Verfügung.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Parst ein Enum-Value in ein Value eines anderen Typen.
        /// Verhalten: Der abgesicherte Modus (strict = true) stellt sicher, dass alle Wert-Index-Paare
        /// des Enums in <typeparamref name="TOut"/> existieren um Laufzeitfehler zu verhindern.
        /// Im ungesicherten Modus (strict = false) wird zuerst anhand des Namens, dann anhand des Index überführt.
        /// </summary>
        /// <typeparam name="TOut">Der neue Typ des Wertes.</typeparam>
        /// <param name="value">Der zu überführende Wert.</param>
        /// <param name="strict"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Das Parsen funktioniert nicht ODER (bei strict = true) nicht alle Ausprägungen des Enums sind im Zielenum enthalten.</exception>
        public static TOut ChangeType<TOut>(this Enum value, bool strict = false)
            where TOut : struct, Enum
        {
            return new EnumHelper(value).ChangeType<TOut>(value, strict);
        }
    }
}