using System.Collections.Generic;

namespace Dataport.AppFrameDotNet.DotNetTools.Linq
{
    /// <summary>
    /// Rückgabe einer Datenseite mit Metadaten über das gesamte Resultset.
    /// </summary>
    /// <typeparam name="TSource">Typ der Datensätze in IQueryable</typeparam>
    public class PagedResult<TSource>
    {
        /// <summary>
        /// Datensätze der aktuellen Seite.
        /// </summary>
        public IEnumerable<TSource> Items { get; internal set; }

        /// <summary>
        /// Anzahl Seiten.
        /// </summary>
        public int PageCount { get; internal set; }

        /// <summary>
        /// Gesamtzahl Datensätze über alle Seiten.
        /// </summary>
        public int TotalItemCount { get; internal set; }

        /// <summary>
        /// Anzahl Datensätze pro Seite.
        /// </summary>
        public int PageSize { get; internal set; }

        /// <summary>
        /// Index (0-basiert) der zurückgegebenen Seite. -1 wenn die angeforderte Seite außerhalb des Gesmatdatenbestands war.
        /// </summary>
        public int CurrentPage { get; internal set; }
    }
}
