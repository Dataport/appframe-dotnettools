using Dataport.AppFrameDotNet.DotNetTools.Validation.Models;

namespace Dataport.AppFrameDotNet.DotNetTools.Validation
{
    /// <summary>
    /// Stellt Validierungen zur Verfügung.
    /// </summary>
    public static class Verify
    {
        /// <summary>
        /// Startet den Validierungszyklus.
        /// </summary>
        /// <param name="objectToVerify">Das Objekt, das geprüft wird.</param>
        /// <param name="nameOfObject">Der Name des Objekts.</param>
        /// <returns>Eine überprüfbare Bedingung.</returns>
        public static Condition<TType> That<TType>(TType objectToVerify, string nameOfObject)
        {
            return new Condition<TType>(objectToVerify, nameOfObject);
        }
    }
}