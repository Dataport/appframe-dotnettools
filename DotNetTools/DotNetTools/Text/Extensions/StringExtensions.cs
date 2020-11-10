using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.Text.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für Strings zur Verfügung.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Dictionary<string, string> LogicalInversions = new Dictionary<string, string>
        {
            { "<", ">" },
            { "/", "\\" },
            { "[", "]" },
            { "(", ")" },
            { "{", "}" },
            { "´", "`" },
        };

        /// <summary>
        /// Konvertiert einen String in einen Base64-String.
        /// </summary>
        /// <param name="str">Der String, der zu konvertieren ist</param>
        /// <param name="encoding">Das Encoding was dafür verwendet werden soll</param>
        /// <returns>Der konvertierte String</returns>
        public static string ToBase64(this string str, Encoding encoding)
        {
            return Convert.ToBase64String(encoding.GetBytes(str));
        }

        /// <summary>
        /// Konvertiert einen Base64-String in einen normalen String.
        /// </summary>
        /// <param name="base64">Der Bas64String der zu konvertieren ist</param>
        /// <param name="encoding">Das Encoding was dafür verwendet werden soll</param>
        /// <returns>Der konvertierte String</returns>
        public static string FromBase64(this string base64, Encoding encoding)
        {
            var bytes = Convert.FromBase64String(base64);
            return encoding.GetString(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Kürzt den String auf eine gegebene Anzahl von Zeichen, so dies notwendigt ist.
        /// </summary>
        /// <param name="str">Der zu kürzende String</param>
        /// <param name="maxLength">Die Maximale Länge des Ergebnisses</param>
        /// <returns>Der gekürzte String.</returns>
        /// <exception cref="InvalidOperationException">Die Länge ist kleiner als 0.</exception>
        public static string Truncate(this string str, int maxLength)
        {
            return (str?.Length ?? 0) <= maxLength ? str : str?.Substring(0, maxLength);
        }

        /// <summary>
        /// Konvertiert einen String in einen Member des übergebenen Enum-Typens.
        /// Verhalten: Der Member muss am Enum definiert sein.
        /// </summary>
        /// <typeparam name="TEnum">Der Typ des Enums</typeparam>
        /// <param name="str">Der zu transformierende String</param>
        /// <param name="ignoreCase">Gibt an, ob die Groß- und Kleinschreibung beim parsen ignoriert werden soll.</param>
        /// <returns>Ein Member des Enums</returns>
        /// <exception cref="ArgumentException">Der String konnte nicht geparst werden oder der Member war für <typeparamref name="TEnum"/> nicht definiert.</exception>
        public static TEnum ToEnumMember<TEnum>(this string str, bool ignoreCase = false) where TEnum : struct, Enum
        {
            if (!str.TryToEnumMember(ignoreCase, out TEnum candidate))
            {
                throw new ArgumentException($"{str} is not defined for {typeof(TEnum)}.");
            }

            return candidate;
        }

        /// <summary>
        /// Versucht einen String in einen Member des übergebenen Enum-Typens zu konvertieren.
        /// Verhalten: Der Member muss am Enum definiert sein.
        /// </summary>
        /// <typeparam name="TEnum">Der Typ des Enums</typeparam>
        /// <param name="str">Der zu transformierende String</param>
        /// <param name="ignoreCase">Gibt an, ob die Groß- und Kleinschreibung beim parsen ignoriert werden soll.</param>
        /// <param name="member">Der konvertierte Member, so das Ergebnis der Methode <see langword="true"/> ist, andernfalls der Defaulwert des Enums.</param>
        /// <returns><see langword="true"/> wenn die Konvertierung erfolgreich ist, andernfalls <see langword="false"/>.</returns>
        public static bool TryToEnumMember<TEnum>(this string str, bool ignoreCase, out TEnum member) where TEnum : struct, Enum
        {
            var enumType = typeof(TEnum);
            if (!Enum.TryParse(str, ignoreCase, out member))
            {
                return false;
            }

            if (!Enum.IsDefined(enumType, member))
            {
                member = default; // andernfalls könnten bspw. für numerische Werte unterschiedliche Ergebnisse zurück kommen
                return false;
            }

            return true;
        }

        /// <summary>
        /// Escaped XML-Characters in dem übergebenen String.
        /// </summary>
        /// <param name="str">Der zu escapende String</param>
        /// <returns>Der escapte String</returns>
        public static string EscapeXml(this string str)
        {
            return SecurityElement.Escape(str);
        }

        /// <summary>
        /// Normalisiert leer Strings auf null/nothing.
        /// </summary>
        /// <param name="str">Der ggf. zu transformierende String</param>
        /// <returns>Der ggf. transformierte String.</returns>
        public static string EmptyAsNull(this string str)
        {
            return string.IsNullOrWhiteSpace(str) ? null : str;
        }

        /// <summary>
        /// Setzt das erste Zeichen des übergebenen Strings in UpperCase.
        /// </summary>
        /// <param name="str">Der zu manipulierende String.</param>
        /// <returns>Der manipulierte String.</returns>
        public static string FirstLetterUpperCase(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        /// <summary>
        /// Entfernt die übergebenen Substrings aus dem String.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <param name="toStrip">Die zu entfernenden Substrings.</param>
        /// <returns>Der bereinigte String.</returns>
        public static string Strip(this string str, params string[] toStrip)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var result = str;
            toStrip?.Where(s => !string.IsNullOrEmpty(s)).ForEach(s => result = result.Replace(s, string.Empty));
            return result;
        }

        /// <summary>
        /// Entfernt umschließende Zeichenfolgen von einem String.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <param name="surrounding">Die umschließende Zeichenfolge</param>
        /// <param name="inverted"><see langword="true"/> wenn die Zeichenfolge am Ende logisch gespiegelt wird.</param>
        /// <returns>Der bereinigte String.</returns>
        public static string StripSurrounding(this string str, string surrounding, bool inverted = false)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(surrounding))
            {
                return str;
            }

            if (str.StartsWith(surrounding) && str.EndsWith(inverted ? surrounding.ReverseLogical() : surrounding))
            {
                return str.Substring(surrounding.Length, str.Length - surrounding.Length * 2);
            }

            return str;
        }

        /// <summary>
        /// Ersetzt sämtliche Zeilenumbrüche (Environment.NewLine, \r\n, \r, \n) durch Leerzeichen.
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <returns>Der bereinigte String.</returns>
        public static string StripLineFeeds(this string str)
        {
            return str?
                .Replace(Environment.NewLine, " ")
                .Replace("\r\n", " ")
                .Replace("\r", " ")
                .Replace("\n", " ")
                .ReplaceRecursive("  ", " ");
        }

        /// <summary>
        /// Bereinigt Texte für Ausgabe in UI-Textbox oder Textdatei.
        /// <br /> \r\n wird in Zeilenumbrüche umgewandelt.
        /// <br /> Umschließende Anführungszeichen werden entfernt
        /// </summary>
        /// <param name="str">Der zu bereinigende String.</param>
        /// <returns>Der bereinigte String.</returns>
        /// <remarks>Hier geht es im allgemeinen um Artefakte die durch JSON-Serialisierung entstehen.</remarks>
        public static string CleanJson(this string str)
        {
            // Die Methode wirkt als Ganzes unausgereift.
            // Die Funktionalität wurde von Version 2 auf 3 portiert, aber gibt noch keine Tests.

            if (str == null)
            {
                return string.Empty;
            }

            str = str.StripSurrounding("\"");

            return str.Replace("\\r\\n", Environment.NewLine);
        }

        /// <summary>
        /// Invertiert einen String und dreht dabei die Zeichen um, zu denen es ein logisches gespiegeltes Gegenüber gibt.
        /// </summary>
        /// <param name="str">Der zu invertierende String.</param>
        /// <returns>Der logisch invertierte String.</returns>
        public static string ReverseLogical(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            var unique = "|%|";
            while (str.Contains(unique))
            {
                unique += "|%|";
            }

            foreach (var inversion in LogicalInversions)
            {
                str = str.Replace(inversion.Key, unique)
                    .Replace(inversion.Value, inversion.Key)
                    .Replace(unique, inversion.Value);
            }

            return string.Join("", str.Reverse());
        }

        /// <summary>
        /// Ersetzt einen Substring innerhalb eines Strings so lange bis der Substring nicht mehr vorhanden ist.
        /// </summary>
        /// <param name="str">Der zu verändernde String.</param>
        /// <param name="oldValue">Der zu ersetzende Wert.</param>
        /// <param name="newValue">Der Wert, mit dem der Originalwert zu ersetzen ist.</param>
        /// <returns>Der ersetzte String.</returns>
        public static string ReplaceRecursive(this string str, string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            string result = str;
            do
            {
                str = result;
                result = str.Replace(oldValue, newValue);
            } while (result.Length != str.Length);

            return result;
        }
    }
}