namespace Dataport.AppFrameDotNet.DotNetTools.Time.Model
{
    /// <summary>
    /// Gibt einen Teil von DateTime an.
    /// </summary>
    public enum DateTimePart
    {
        /// <summary>
        /// Millisekunde
        /// </summary>
        Milliseconds = 1,

        /// <summary>
        /// Sekunde
        /// </summary>
        Seconds = 2,

        /// <summary>
        /// Minute
        /// </summary>
        Minutes = 3,

        /// <summary>
        /// Stunde
        /// </summary>
        Hours = 4,

        /// <summary>
        /// Tag
        /// </summary>
        Days = 5,

        /// <summary>
        /// Monat
        /// </summary>
        Months = 6,

        /// <summary>
        /// Jahr
        /// </summary>
        Year = 7
    }
}