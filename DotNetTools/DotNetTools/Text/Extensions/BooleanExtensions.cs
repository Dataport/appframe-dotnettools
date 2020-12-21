namespace Dataport.AppFrameDotNet.DotNetTools.Text.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für Strings zur Verfügung.
    /// </summary>
    public static class BooleanExtensions
    {
        /// <summary>
        /// Wandelt das übergebene bool in einen lesbaren String um.
        /// </summary>
        /// <param name="value">Der gesetzte Wert.</param>
        /// <param name="ifTrue">Das Ergebnis, falls der Wert <see langword="true"/> ist.</param>
        /// <param name="ifFalse">Das Ergebnis, falls der Wert <see langword="false"/> ist.</param>
        /// <returns></returns>
        public static string ToYesNo(this bool value, string ifTrue = "Yes", string ifFalse = "No")
        {
            return value ? ifTrue : ifFalse;
        }

        /// <summary>
        /// Wandelt das übergebene bool in einen lesbaren String um.
        /// Verhalten: <see langword="null"/> wird als <see langword="false"/> angesehen.
        /// </summary>
        /// <param name="value">Der gesetzte Wert.</param>
        /// <param name="ifTrue">Das Ergebnis, falls der Wert <see langword="true"/> ist.</param>
        /// <param name="ifFalse">Das Ergebnis, falls der Wert <see langword="false"/> oder Das Ergebnis, falls der Wert <see langword="null"/> ist.</param>
        /// <returns></returns>
        public static string ToYesNo(this bool? value, string ifTrue = "Yes", string ifFalse = "No")
        {
            return value.GetValueOrDefault().ToYesNo(ifTrue, ifFalse);
        }
    }
}