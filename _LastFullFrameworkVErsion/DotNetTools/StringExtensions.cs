using System;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Extensions für Strings.
    /// </summary>
    /// <remarks></remarks>
    public static class StringExtensions
    {

        /// <summary>
        /// Kombiniert die Einträge der Enumeration zu einem String mit dem angegebenem Trennzeichen
        /// zwischen den Items.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trenner"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string StringConcat(this IEnumerable<string> context, string trenner)
        {
            if (context == null)
                return "";
            if (trenner == null)
                trenner = "";

            return string.Join(trenner, context.ToArray());
        }

        /// <summary>
        /// Kombiniert die nicht leeren Einträge der Enumeration zu einem String mit dem angegebenem Trennzeichen
        /// zwischen den Items.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trenner"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string StringConcatNotEmpty(this IEnumerable<string> context, string trenner)
        {
            if (context == null)
                return "";
            if (trenner == null)
                trenner = "";

            return string.Join(trenner, context.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
        }

        /// <summary>
        /// Entfernt Return und Linefeed aus einem String um Zeilenumbrüche zu vermeiden.
        /// Return wird durch ein Leerzeichen ersetzt um Worttrennungen zu erhalten.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string NoReturnOrLinefeed(this string context)
        {
            return context?.Replace("\r", " ").Replace("\n", "");
        }

        /// <summary>
        /// String auf eine Länge begrenzen.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        /// <remarks>Nein das gibs tatsächlich nicht bereits im .NET-Framework!</remarks>
        public static string Truncate(this string context, int length)
        {
            //Nix is nix
            if (context == null)
                return null;
            //Hier wird die ArgumentOutOfRange-Exception vermieden
            if (context.Length <= length)
                return context;
            //Truncate
            return context.Substring(0, length);
        }

        /// <summary>
        /// Bereinigt Texte für Ausgabe in UI-Textbox oder Textdatei.
        /// <br /> \r\n wird in Zeilenumbrüche umgewandelt.
        /// <br /> Umschließende Anführungszeichen werden entfernt
        /// </summary>
        /// <remarks>Hier geht es im allgemeinen um Artefakte die durch JSON-Serialisierung entstehen.</remarks>
        public static string AsCleanDisplayText(this string context)
        {
            if (context == null)
                return string.Empty;

            if (context.First() == '"' & context.Last() == '"')
            {
                context = context.Substring(1, context.Length - 2);
            }

            return context.Replace("\\r\\n", Environment.NewLine);
        }

        /// <summary>
        /// Normalisiert leer Strings auf null/nothing.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string EmptyAsNull(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
                return null;

            return context;
        }
    }
}
