using Dataport.AppFrameDotNet.DotNetTools.Validation.Models;
using System.Collections.Generic;

namespace Dataport.AppFrameDotNet.DotNetTools.Validation
{
    /// <summary>
    /// Repräsentiert das Ergebnis einer Validierung
    /// </summary>
    public class ValidationResults
    {
        /// <summary>
        /// Gibt den Schweregrad der Validierung zurück.
        /// </summary>
        public Severity Severity { get; private set; } = Severity.Success;

        /// <summary>
        /// Gibt eine Auflistung aller Fehlermeldungen zurück.
        /// </summary>
        public string[] Errors => _errors.ToArray();

        /// <summary>
        /// Gibt eine Auflistung aller Warnungen zurück
        /// </summary>
        public string[] Warnings => _warnings.ToArray();

        /// <summary>
        /// Gibt eine Auflistung aller Informationen zurück.
        /// </summary>
        public string[] Information => _infos.ToArray();

        private readonly List<string> _errors = new List<string>();

        private readonly List<string> _warnings = new List<string>();

        private readonly List<string> _infos = new List<string>();

        /// <summary>
        /// Fügt eine Fehlermeldung hinzu.
        /// </summary>
        /// <param name="message">Die Fehlermeldung</param>
        public void AddError(string message)
        {
            Severity = Severity.Error;
            _errors.Add(message);
        }

        /// <summary>
        /// Fügt eine Warnungsmeldung hinzu.
        /// </summary>
        /// <param name="message">Die Warnungsmeldung</param>
        public void AddWarning(string message)
        {
            if (Severity != Severity.Error)
            {
                Severity = Severity.Warning;
            }
            _warnings.Add(message);
        }

        /// <summary>
        /// Fügt eine Informationsmeldung hinzu.
        /// </summary>
        /// <param name="message">Die Informationsmeldung</param>
        public void AddInformation(string message)
        {
            if (Severity == Severity.Success)
            {
                Severity = Severity.Information;
            }
            _infos.Add(message);
        }
    }
}