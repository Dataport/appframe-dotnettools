using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Stellt Methoden zum Filtern zur Verfügung.
    /// </summary>
    public static class Filtering
    {
        /// <summary>
        /// Gibt nur eindeutige Elemente aus <paramref name="enumerable"/> anhand des <paramref name="selector"/> zurück.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <typeparam name="TIdentifier">Der Typ der Identifiers.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <param name="selector">Eine Funktion um zu ermitteln, woran die Eindeutigkeit festgemacht werden kann.</param>
        /// <returns>Alle eindeutigen Elemente der Enumeration</returns>
        public static IEnumerable<TType> Distinct<TType, TIdentifier>(this IEnumerable<TType> enumerable, Func<TType, TIdentifier> selector)
        {
            List<TIdentifier> existing = new List<TIdentifier>();
            foreach (TType element in enumerable)
            {
                TIdentifier elementSelector = selector(element);
                if (existing.Contains(elementSelector))
                {
                    continue;
                }

                existing.Add(elementSelector);
                yield return element;
            }
        }

        /// <summary>
        /// Filtert die Elemente aus einer Liste, die <see langword="null"/> sind.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Enumeration.</typeparam>
        /// <param name="enumerable">Die Enumeration</param>
        /// <returns>Die gefilterte Liste.</returns>
        public static IEnumerable<TType> WhereNotNull<TType>(this IEnumerable<TType> enumerable)
        {
            return enumerable.Where(e => e != null);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<string> WhereLike(this IEnumerable<string> enumerable, string filter, char wildcard = '*')
        {
            return enumerable.WhereLike(x => x, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<string> WhereLike(this IQueryable<string> queryable, string filter, char wildcard = '*')
        {
            return queryable.WhereLike(x => x, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Elements</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TType> WhereLike<TType>(this IEnumerable<TType> enumerable, Expression<Func<TType, string>> valueSelector, string filter, char wildcard = '*')
        {
            return enumerable.AsQueryable().WhereLike(valueSelector, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Elements</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TType> WhereLike<TType>(this IQueryable<TType> queryable, Expression<Func<TType, string>> valueSelector, string filter, char wildcard = '*')
        {
            return queryable.Where(ExpressionHelper.CreateLikeExpression(valueSelector, filter, wildcard));
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen. Ist der Filter leer, wird nicht gefiltert.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<string> WhereLikeOptional(this IEnumerable<string> enumerable, string filter, char wildcard = '*')
        {
            return enumerable.WhereLikeOptional(x => x, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen. Ist der Filter leer, wird nicht gefiltert.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<string> WhereLikeOptional(this IQueryable<string> queryable, string filter, char wildcard = '*')
        {
            return queryable.WhereLikeOptional(x => x, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen. Ist der Filter leer, wird nicht gefiltert.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Elements</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TType> WhereLikeOptional<TType>(this IEnumerable<TType> enumerable, Expression<Func<TType, string>> valueSelector, string filter, char wildcard = '*')
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return enumerable;
            }

            return enumerable.WhereLike(valueSelector, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem Like-Statement entsprechen. Ist der Filter leer, wird nicht gefiltert.
        /// Verhalten: Wildcards werden nur am Anfang und Ende unterstützt. Elemente in der Liste dürfen nicht null sein.
        /// </summary>
        /// <typeparam name="TType">Der Typ des Elements</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TType> WhereLikeOptional<TType>(this IQueryable<TType> queryable, Expression<Func<TType, string>> valueSelector, string filter, char wildcard = '*')
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return queryable;
            }

            return queryable.WhereLike(valueSelector, filter, wildcard);
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<string> WhereEquals(this IEnumerable<string> enumerable, string value)
        {
            return enumerable.AsQueryable().WhereEquals(value);
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<string> WhereEquals(this IQueryable<string> queryable, string value)
        {
            return queryable.Where(ExpressionHelper.CreateEqualExpression<string, string>(x => x, value));
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Instanz</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TType> WhereEquals<TType>(this IEnumerable<TType> enumerable, TType value) where TType : struct
        {
            return enumerable.WhereEquals(x => x, value);
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Instanz</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TType> WhereEquals<TType>(this IQueryable<TType> queryable, TType value) where TType : struct
        {
            return queryable.WhereEquals(x => x, value);
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TInstance> WhereEquals<TInstance>(this IEnumerable<TInstance> enumerable, Expression<Func<TInstance, string>> valueSelector, string value)
        {
            return enumerable.AsQueryable().WhereEquals(valueSelector, value);
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TInstance> WhereEquals<TInstance>(this IQueryable<TInstance> queryable, Expression<Func<TInstance, string>> valueSelector, string value)
        {
            return queryable.Where(ExpressionHelper.CreateEqualExpression(valueSelector, value));
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <typeparam name="TElement">Der Typ des zu vergleichenden Elements</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TInstance> WhereEquals<TInstance, TElement>(this IEnumerable<TInstance> enumerable, Expression<Func<TInstance, TElement>> valueSelector, TElement value)
            where TElement : struct
        {
            return enumerable.AsQueryable().WhereEquals(valueSelector, value);
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <typeparam name="TElement">Der Typ des zu vergleichenden Elements</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TInstance> WhereEquals<TInstance, TElement>(this IQueryable<TInstance> queryable, Expression<Func<TInstance, TElement>> valueSelector, TElement value)
            where TElement : struct
        {
            return queryable.Where(ExpressionHelper.CreateEqualExpression(valueSelector, value));
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen. Ist der Wert <see langword="null"/>, so wird nicht gefiltert.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Instanz</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TType> WhereEqualsOptional<TType>(this IEnumerable<TType> enumerable, TType? value) where TType : struct
        {
            return value.HasValue ? enumerable.WhereEquals(x => x, value.Value) : enumerable;
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen. Ist der Wert <see langword="null"/>, so wird nicht gefiltert.
        /// </summary>
        /// <typeparam name="TType">Der Typ der Instanz</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TType> WhereEqualsOptional<TType>(this IQueryable<TType> queryable, TType? value) where TType : struct
        {
            return value.HasValue ? queryable.WhereEquals(x => x, value.Value) : queryable;
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen. Ist der Wert <see langword="null"/>, so wird nicht gefiltert.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <typeparam name="TElement">Der Typ des zu vergleichenden Elements</typeparam>
        /// <param name="enumerable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IEnumerable<TInstance> WhereEqualsOptional<TInstance, TElement>(this IEnumerable<TInstance> enumerable, Expression<Func<TInstance, TElement>> valueSelector, TElement? value)
            where TElement : struct
        {
            return value.HasValue ? enumerable.WhereEquals(valueSelector, value.Value) : enumerable;
        }

        /// <summary>
        /// Filtert alle Einträge die dem übergebenen Wert entsprechen. Ist der Wert <see langword="null"/>, so wird nicht gefiltert.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <typeparam name="TElement">Der Typ des zu vergleichenden Elements</typeparam>
        /// <param name="queryable">Die zu filternde Auflistung</param>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="value">Der Vergleichswert</param>
        /// <returns>Die gefilterte Liste</returns>
        public static IQueryable<TInstance> WhereEqualsOptional<TInstance, TElement>(this IQueryable<TInstance> queryable, Expression<Func<TInstance, TElement>> valueSelector, TElement? value)
            where TElement : struct
        {
            return value.HasValue ? queryable.WhereEquals(valueSelector, value.Value) : queryable;
        }
    }
}