using System;
using System.Collections.Generic;
using System.Linq;

namespace Dataport.AppFrameDotNet.DotNetTools.Linq
{
    /// <summary>
    /// Erweiterungsmethoden für Paging in Linq.
    /// </summary>
    /// <remarks></remarks>
    public static class PagingExtensions
    {
      
        /// <summary>
        /// Hilfsmethode um große Datenmengen in Pages zerlegt durchzugehen.
        /// </summary>
        /// <typeparam name="TSource">Typ der Datensätze in IOrderedQueryable</typeparam>
        /// <param name="context">Query</param>
        /// <param name="pageSize">Anzahl Datensätze pro Seite</param>
        /// <param name="requestedPage">Angeforderte Seite (nullbasierter Index)</param>
        /// <returns>Rückgabeobjekt mit den Daten einer Datenseite (Page), der Anzahl der Seiten und der Gesamtzahl der Datensätze.</returns>
        /// <remarks></remarks>
        public static PagedResult<TSource> Page<TSource>(this IOrderedQueryable<TSource> context, int pageSize,
            int requestedPage)
        {
            return context.PageInternal(pageSize, requestedPage);
        }

        /// <summary>
        /// Hilfsmethode um große Datenmengen in Pages zerlegt durchzugehen.
        /// </summary>
        /// <typeparam name="TSource">Typ der Datensätze in IOrderedQueryable</typeparam>
        /// <param name="context">Query</param>
        /// <param name="pageSize">Anzahl Datensätze pro Seite</param>
        /// <param name="requestedPage">Angeforderte Seite (nullbasierter Index)</param>
        /// <returns>Rückgabeobjekt mit den Daten einer Datenseite (Page), der Anzahl der Seiten und der Gesamtzahl der Datensätze.</returns>
        /// <remarks></remarks>
        public static PagedResult<TSource> Page<TSource>(this IOrderedEnumerable<TSource> context, int pageSize,
            int requestedPage)
        {
            return context.PageInternal(pageSize, requestedPage);
        }

        private static PagedResult<TSource> PageInternal<TSource>(this IEnumerable<TSource> context, int pageSize, int requestedPage)
        {
            //PageSize macht mit negativem Wert oder 0 keinen Sinn
            if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize muss ein positiver Wert sein.");

            //Ein negativer Index für die Page macht keinen Sinn
            if (requestedPage < 0) throw new ArgumentOutOfRangeException(nameof(pageSize), "requestedPage muss größer 0 sein.");

            //Gesamtzahl der Datensätze ermitteln
            // ReSharper disable once PossibleMultipleEnumeration
            var totalItemCount = context.Count();

            //Anzahl Seiten ermitteln
            var pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(totalItemCount) / pageSize));

            //Datensätze für Seite ermitteln
            // ReSharper disable once PossibleMultipleEnumeration
            var items = context.Skip(requestedPage * pageSize).Take(pageSize).ToArray();

            //Wenn die angeforderte Seite leer ist, sind wir außerhalb des möglichen Rückgabebereichs
            var currentPage = !items.Any() ? -1 : requestedPage;

            //Rückgabe
            return new PagedResult<TSource>() { Items = items, PageCount = pageCount, TotalItemCount = totalItemCount, PageSize = pageSize, CurrentPage = currentPage};
        }
    }
}
