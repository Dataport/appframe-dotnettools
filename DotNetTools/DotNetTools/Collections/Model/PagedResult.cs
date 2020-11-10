using System.Collections.Generic;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Model
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
        public IEnumerable<TSource> Items { get; }

        /// <summary>
        /// Anzahl Seiten.
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        /// Gesamtzahl Datensätze über alle Seiten.
        /// </summary>
        public int TotalItemCount { get; }

        /// <summary>
        /// Anzahl Datensätze pro Seite.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Index (0-basiert) der zurückgegebenen Seite. -1 wenn die angeforderte Seite außerhalb des Gesmatdatenbestands war.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Initialisiert das Model
        /// </summary>
        /// <param name="items">Datensätze der aktuellen Seite.</param>
        /// <param name="pageCount">Anzahl Seiten.</param>
        /// <param name="totalItemCount">Gesamtzahl Datensätze über alle Seiten.</param>
        /// <param name="pageSize">Anzahl Datensätze pro Seite.</param>
        /// <param name="currentPage">Index (0-basiert) der zurückgegebenen Seite. -1 wenn die angeforderte Seite außerhalb des Gesmatdatenbestands war.</param>
        public PagedResult(IEnumerable<TSource> items, int pageCount, int totalItemCount, int pageSize, int currentPage)
        {
            Items = items;
            PageCount = pageCount;
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
            CurrentPage = currentPage;
        }
    }
}