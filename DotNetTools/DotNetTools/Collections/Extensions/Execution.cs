using System;
using System.Collections.Generic;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Ausführen von Anweisungen zur Verfügung.
    /// </summary>
    public static class Execution
    {
        /// <summary>
        /// Führt die angegebene Aktion für jedes Element der Enumeration aus.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration</typeparam>
        /// <param name="enumerable">Die zu durchlaufende Enumeration.</param>
        /// <param name="action">Die Aktion die ausgeführt werden soll.</param>
        public static void ForEach<TType>(this IEnumerable<TType> enumerable, Action<TType> action)
        {
            foreach (var element in enumerable)
            {
                action(element);
            }
        }
    }
}