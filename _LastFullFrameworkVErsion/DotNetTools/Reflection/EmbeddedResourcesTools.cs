using System;
using System.IO;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection
{
    /// <summary>
    /// Hilfsmethoden für den Umgang mit EmbeddedRessources.
    /// </summary>
    public static class EmbeddedResourcesTools
    {
        /// <summary>
        /// Liest die angegebene Ressource als Textdatei.
        /// </summary>
        /// <param name="assembly">Assemlby, in der die Ressource eingebettet ist.</param>
        /// <param name="filename">Dateiname mit Pfad ([Root Namespace].[Unterordner].[Dateiname])</param>
        /// <returns></returns>
        public static string GetEmbeddedTextFile(Assembly assembly, string filename)
        {
            using (var stream = assembly.GetManifestResourceStream(filename))
            {
                if (stream==null) throw new ArgumentOutOfRangeException(nameof(filename));

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

    }
}
