using System;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections
{
    /// <summary>
    /// Helfer für generische Listen.
    /// </summary>
    /// <remarks></remarks>
    public static class ListTools
    {

        /// <summary>
        /// Ersetzt einen Eintrag in einer Liste durch einen anderen wenn dieser bereits vorhanden ist.
        /// Ansonsten wir er einfach zugefügt.
        /// </summary>
        /// <typeparam name="T">Typ des Listenobjekts</typeparam>
        /// <param name="context">Liste</param>
        /// <param name="selector">Regel zum selektieren des Eintrags. Muss ein einwertiges Ergebnis liefern.</param>
        /// <param name="replacement">Ersatzobjekt</param>
        /// <remarks></remarks>
        public static void AddOrReplace<T>(this List<T> context, Func<T, bool> selector, T replacement)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));
            if (replacement == null)
                throw new ArgumentNullException(nameof(replacement));

            var existing = context.SingleOrDefault(selector);

            if (existing != null)
            {
                context.Remove(existing);
            }

            context.Add(replacement);
        }

    }
}
