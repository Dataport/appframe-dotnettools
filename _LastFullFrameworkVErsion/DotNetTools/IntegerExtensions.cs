namespace Dataport.AppFrameDotNet.DotNetTools
{
    /// <summary>
    /// Extensions für den Datentyp 'int'.
    /// </summary>
    public static class IntegerExtensions
    {

        /// <summary>
        /// Für IF() oder Linq-Funktionen, damit der richtige Type "Infered" wird.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int? AsNullable(this int context)
        {
            return context;
        }

    }
}
