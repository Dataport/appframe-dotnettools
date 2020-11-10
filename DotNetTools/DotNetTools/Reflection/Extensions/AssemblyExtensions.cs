using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Verarbeiten von Resourcen zur Verfügung.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Lädt den Inhalt einer Datei die als eingebettete Resource im System verfügbar ist.
        /// Ist der Name der Datei im Assembly eindeutig, so kann im Pfad nur der Datei-Name eingetragen werden.
        /// </summary>
        /// <param name="assembly">Die Assembly die erweitert wird.</param>
        /// <param name="filePath">Der Weg zur Datei.</param>
        /// <returns>Den Inhalt der Datei.</returns>
        public static string LoadEmbeddedResource(this Assembly assembly, string filePath)
        {
            using (var stream = assembly.GetManifestResourceStream(filePath))
            {
                if (stream == null)
                {
                    try
                    {
                        string resourcePath = assembly.GetManifestResourceNames().Single(str => str.EndsWith(filePath));
                        return assembly.LoadEmbeddedResource(resourcePath);
                    }
                    catch (Exception)
                    {
                        throw new ArgumentOutOfRangeException(nameof(filePath));
                    }
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}