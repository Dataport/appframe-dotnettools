using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection
{
    /// <summary>
    /// Stellt typsichere Enum-Methoden zur Verfügung.
    /// </summary>
    /// <typeparam name="TEnum">Der Typ des Enums</typeparam>
    [SuppressMessage("ReSharper", "StaticMemberInGenericType", Justification = "Macht hier durchaus Sinn.")]
    public static class EnumHelper<TEnum> where TEnum : Enum
    {
        private static readonly EnumHelper Helper = new EnumHelper(default(TEnum));

        /// <summary>
        /// Gibt die Anzahl der Enum-Values zurück.
        /// </summary>
        /// <returns>Die Anzahl der Values.</returns>
        public static int Count()
        {
            return Helper.Count();
        }

        /// <summary>
        /// Gibt alle Values des Enums zurück.
        /// </summary>
        /// <returns>Alle Values des Enums.</returns>
        public static IEnumerable<TEnum> GetValues()
        {
            return Helper.GetValues().Cast<TEnum>();
        }

        /// <summary>
        /// Gibt alle Values des Enums anhand eines Predikats über ein Attribut an den Membern zurück.
        /// Verhalten: Member die nicht über das Attribut verfügen werden ignoriert.
        /// </summary>
        /// <typeparam name="TAttribute">Der Typ des Attributs</typeparam>
        /// <param name="condition">Bedingung, welche die Attribute erfüllen müssen.</param>
        /// <returns>Alle Values deren Attribute die übergebene Bedingung erfüllen.</returns>
        public static IEnumerable<TEnum> GetValuesWhereAttribute<TAttribute>(Func<TAttribute, bool> condition) where TAttribute : Attribute
        {
            return Helper.GetValuesWhereAttribute(condition).Cast<TEnum>();
        }

        /// <summary>
        /// Überführt das Enum in ein Dictionary.
        /// </summary>
        /// <returns>Die Dictionary-Representation des Enums</returns>
        public static IDictionary<int, string> AsDictionary()
        {
            return Helper.AsDictionary();
        }

        /// <summary>
        /// Überführt das Enum in ein Dictionary.
        /// </summary>
        /// <typeparam name="TKey">Der Key-Typ des Enums</typeparam>
        /// <returns>Die Dictionary-Representation des Enums</returns>
        public static IDictionary<TKey, string> AsDictionary<TKey>()
        {
            return Helper.AsDictionary<TKey>();
        }

        /// <summary>
        /// Gibt an, ob das Enum vollständig kongruent zu einem anderen Enum ist.
        /// </summary>
        /// <typeparam name="TOther">Der Typ des anderen Enums</typeparam>
        /// <returns><see langword="true"/> wenn die Enums kongruent sind, andernfalls <see langword="false"/></returns>
        public static bool IsSubstituteOf<TOther>() where TOther : Enum
        {
            return Helper.IsSubstituteOf<TOther>();
        }

        /// <summary>
        /// Gibt an, ob das Enum mit all seinen Index-Wert-Kombinationen vollständig von einem anderes Enum abgedeckt wird.
        /// </summary>
        /// <typeparam name="TOther">Der Typ des anderen Enums</typeparam>
        /// <returns><see langword="true"/> wenn das Enum passt, andernfalls <see langword="false"/></returns>
        public static bool FitsInto<TOther>() where TOther : Enum
        {
            return Helper.FitsInto<TOther>();
        }

        /// <summary>
        /// Gibt an, ob das Enum mit all seinen Namen vollständig von einem anderen Enum abgedeckt wird.
        /// </summary>
        /// <typeparam name="TOther">Der Typ des anderen Enums</typeparam>
        /// <returns><see langword="true"/> wenn das Enum passt, andernfalls <see langword="false"/></returns>
        public static bool FitsByNameInto<TOther>() where TOther : Enum
        {
            return Helper.FitsByNameInto<TOther>();
        }

        /// <summary>
        /// Gibt an, ob das Enum mit all seinen Indexen vollständig von einem anderen Enum abgedeckt wird.
        /// </summary>
        /// <typeparam name="TOther">Der Typ des anderen Enums</typeparam>
        /// <returns><see langword="true"/> wenn das Enum passt, andernfalls <see langword="false"/></returns>
        public static bool FitsByIndexInto<TOther>() where TOther : Enum
        {
            return Helper.FitsByIndexInto<TOther>();
        }

        /// <summary>
        /// Parst ein Enum-Value in ein Value eines anderen Typen.
        /// Verhalten: Der abgesicherte Modus (strict = true) stellt sicher, dass alle Wert-Index-Paare
        /// von <typeparamref name="TEnum"/> in <typeparamref name="TOut"/> existieren um Laufzeitfehler zu verhindern.
        /// Im ungesicherten Modus (strict = false) wird zuerst anhand des Namens, dann anhand des Index überführt.
        /// </summary>
        /// <typeparam name="TOut">Der neue Typ des Wertes.</typeparam>
        /// <param name="value">Der zu überführende Wert.</param>
        /// <param name="strict"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Das Parsen funktioniert nicht ODER (bei strict = true) nicht alle Ausprägungen des Enums sind im Zielenum enthalten.</exception>
        public static TOut ChangeType<TOut>(TEnum value, bool strict = false)
            where TOut : struct, Enum
        {
            return Helper.ChangeType<TOut>(value, strict);
        }
    }
}