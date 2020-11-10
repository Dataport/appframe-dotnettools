namespace Dataport.AppFrameDotNet.DotNetTools.Comparison.Model
{
    /// <summary>
    /// Gibt an, wie mit Grenzwerten umzugehen ist.
    /// </summary>
    public enum BoundaryType
    {
        /// <summary>
        /// Alle Werte innerhalb der Grenzwerte sind gültig, die Grenzwerte inklusive.
        /// </summary>
        Inclusive,

        /// <summary>
        /// Alle Werte innerhalb der Grenzwerte sind gültig, die Grenzwerte exklusive.
        /// </summary>
        Exclusive,

        /// <summary>
        /// Alle Werte innerhalb der Grenzwerte sind gültig, der untere Grenzwert inklusive.
        /// </summary>
        LowerOnly,

        /// <summary>
        /// Alle Werte innerhalb der Grenzwerte sind gültig, der obere Grenzwert inklusive.
        /// </summary>
        UpperOnly
    }
}