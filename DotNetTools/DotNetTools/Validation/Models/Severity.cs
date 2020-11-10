namespace Dataport.AppFrameDotNet.DotNetTools.Validation.Models
{
    /// <summary>
    /// Gibt den Schweregrad einer Validierungsmeldung an.
    /// </summary>
    public enum Severity
    {
        /// <summary>
        /// Gibt an, dass die Validierung erfolgreich abgeschlossen wurde.
        /// </summary>
        Success,

        /// <summary>
        /// Gibt an, dass die Validierung erfolgreich abgeschlossen wurde und weitere Informationen zur Verfügung stehen.
        /// </summary>
        Information,

        /// <summary>
        /// Gibt an, dass die Validierung erfolgreich abgeschlossen wurde, jedoch Warnungen existieren.
        /// </summary>
        Warning,

        /// <summary>
        /// Gibt an, dass die Validierung fehlerhaft abgeschlossen wurde.
        /// </summary>
        Error
    }
}