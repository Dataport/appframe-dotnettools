using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Dataport.AppFrameDotNet.DotNetTools.Text.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für Strings zur Verfügung.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Konvertiert einen String in einen Member des übergebenen Enum-Typens.
        /// Verhalten: Der Member muss am Enum definiert sein. Zuerst wird auf die genaue Übereinstimmung geprüft,
        /// danach wird auf die Übereinstimmung mit einem Attribut überprüft.
        /// </summary>
        /// <typeparam name="TEnum">Der Typ des Enums</typeparam>
        /// <typeparam name="TAttribute">Der Typ des Attributs</typeparam>
        /// <param name="str">Der zu transformierende String</param>
        /// <param name="ignoreCase">Gibt an, ob die Groß- und Kleinschreibung beim parsen ignoriert werden soll.</param>
        /// <param name="attributeAccessor">Bildungsvorschrift zur Extraktion eines Vergleichwertes aus dem Attribut.</param>
        /// <returns>Ein Member des Enums</returns>
        /// <exception cref="ArgumentException">Der String konnte nicht geparst werden oder der Member war für <typeparamref name="TEnum"/> nicht definiert.</exception>
        /// <exception cref="AmbiguousMatchException">Über die Bildungsvorschrift des Accessors war nicht eindeutig.</exception>
        [SuppressMessage("ReSharper", "UnthrowableException", Justification = "Diese Methoden benutzt Reflection, die darf Reflection-Exceptions werfen.")]
        public static TEnum ToEnumMember<TEnum, TAttribute>(this string str, bool ignoreCase, Func<TAttribute, string> attributeAccessor)
            where TEnum : struct, Enum
            where TAttribute : Attribute
        {
            if (str.TryToEnumMember(ignoreCase, out TEnum member))
            {
                return member;
            }

            var membersViaAttribute = EnumHelper<TEnum>.GetValuesWhereAttribute<TAttribute>(attribute =>
                ignoreCase
                    ? attributeAccessor(attribute).ToLower() == str.ToLower()
                    : attributeAccessor(attribute) == str)
                .ToArray();

            if (membersViaAttribute.Length == 0)
            {
                throw new ArgumentException($"{str} is not defined for {typeof(TEnum)}.");
            }

            if (membersViaAttribute.Length > 1)
            {
                throw new AmbiguousMatchException($"Multiple elements found for {str}.");
            }

            return membersViaAttribute.Single();
        }
    }
}