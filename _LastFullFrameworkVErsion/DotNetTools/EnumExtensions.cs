using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Extension-Methoden zum leichteren Umgang mit Enums.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Ermittelt den benutzerfreundlichen Namen des angegebenen Enum-Wertes.
        /// </summary>
        /// <typeparam name="TEnum">Typ der Enumeration.</typeparam>
        /// <param name="context">Wert der Enumeration, dessen Name ermittelt werden soll.</param>
        /// <returns>Den benutzerfreundlichen Namen des angegebenen Enum-Wertes.</returns>
        /// <remarks>Berücksichtigt das Display-Attribut.</remarks>
        public static string ToDisplayName<TEnum>(this TEnum context)
            where TEnum : struct, IConvertible
        {
            // Generic Constraint so gut wie möglich von:
            // http://stackoverflow.com/questions/79126/create-generic-method-constraining-t-to-an-enum
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum muss eine Enumeration sein.");

            var enumMemberName = Enum.GetName(typeof(TEnum), context);

            // Kann eintreten, wenn die Werte der Enumeration manuell angegeben wurden und nicht bei 0 beginnen.
            // Es ist dann trotzdem möglich, dass in einer Variablen vom Typ der Enum eine 0 steht.
            // In dem Fall hat der Wert keinen gültigen Namen und es wird string.Empty zurückgegeben.
            if (enumMemberName == null)
            {
                return string.Empty;
            }

            var enumMember = typeof(TEnum).GetMember(enumMemberName).FirstOrDefault();
            var displayAttribute = enumMember.GetCustomAttributes<DisplayAttribute>().FirstOrDefault();

            return displayAttribute?.GetName() ?? enumMemberName;
        }

        /// <summary>
        /// Gibt alle Einträge eines Enums zum Durchiterieren zurück.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<T> GetEnumValues<T>()
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException("TEnum muss eine Enumeration sein.");

            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}