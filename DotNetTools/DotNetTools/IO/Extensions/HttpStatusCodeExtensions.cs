using System.Net;
using Dataport.AppFrameDotNet.DotNetTools.Validation;
using Dataport.AppFrameDotNet.DotNetTools.Validation.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.IO.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Auswerten von <see cref="HttpStatusCode"/>s bereit.
    /// </summary>
    public static class HttpStatusCodeExtensions
    {
        /// <summary>
        /// Gibt an, ob der Statuscode einen Erfolg ausdrückt oder nicht.
        /// </summary>
        /// <param name="statusCode">Der zu prüfende Statuscode</param>
        /// <returns><see langword="true"/> falls der Statuscode einen Erfolg ausdrückt, andernfalls <see langword="false"/></returns>
        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
        {
            Verify.That(statusCode, "StatusCode").IsDefined();

            return (int)statusCode >= 200 && (int)statusCode <= 299;
        }
    }
}