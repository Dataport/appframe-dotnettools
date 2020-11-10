using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dataport.AppFrameDotNet.DotNetTools.Linq
{
    /// <summary>
    /// Erweiterungsmethoden für standardisierte Filtermethoden in Linq.
    /// </summary>
    public static class FilterExtensions
    {

        /// <summary>
        /// Wendet einen optionalen Filter an wenn ein Wert angegeben wurde.
        /// Wenn [filter] nicht leer dann Filter auf [targetSelector] = [filter].
        /// </summary>
        /// <typeparam name="TSource">Typ der Entity des IQueryable</typeparam>
        /// <typeparam name="TFilter">Typ der Property auf die gefiltert wird</typeparam>
        /// <param name="context">IQueryable das gefiltert werden soll</param>
        /// <param name="valueSelector">Lambda-Expression mit Auswahl der Property auf die gefilter werden soll</param>
        /// <param name="value">Wert auf den gefiltert werden soll</param>
        /// <returns>Das gefilterte IQueryable</returns>
        /// <remarks></remarks>
        public static IQueryable<TSource> ApplyOptionalEqualityFilter<TSource, TFilter>(this IQueryable<TSource> context, Expression<Func<TSource, TFilter>> valueSelector, TFilter? value) where TFilter : struct
        {
            return value.HasValue ? context.Where(CreateEqualExpression(valueSelector, value.Value)) : context;
        }

        /// <summary>
        /// Wendet einen optionalen Filter an wenn ein Wert angegeben wurde.
        /// Diese Variante gilt für Strings und benutzt "WhereLinke" als Filtermechanismus.
        /// Wenn [filter] nicht leer (Nothing, Empty, Whitspace) dann Filter auf [targetSelector] like [filter].
        /// </summary>
        /// <typeparam name="TBo">Typ der Entity des IQueryable</typeparam>
        /// <param name="context">IQueryable das gefiltert werden soll</param>
        /// <param name="valueSelector">Lambda-Expression mit Auswahl der Property auf die gefilter werden soll</param>
        /// <param name="value">Wert auf den gefiltert werden soll</param>
        /// <returns>Das gefilterte IQueryable</returns>
        /// <remarks></remarks>
        public static IQueryable<TBo> ApplyOptionalWhereLikeFilter<TBo>(this IQueryable<TBo> context, Expression<Func<TBo, string>> valueSelector, string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? context.WhereLike(valueSelector, value) : context;
        }

        /// <summary>
        /// Wendet einen optionalen Filter an, wenn ein Wert angegeben wurde.
        /// Diese allgemeine Variante erlaubt die Definition beliebiger Filter.
        /// Zum Erkennen, ob ein Filter angegeben wurde, wird ein Vergleich gegen den default des Typs angewendet.
        /// </summary>
        /// <typeparam name="TBo">Typ der Entity des IQueryable</typeparam>
        /// <typeparam name="TFilterValue">Typ des Filter-Werts.</typeparam>
        /// <param name="context">IQueryable das gefiltert werden soll</param>
        /// <param name="predicate">Filter, der angewendet werden sollen, wenn ein Wert angegeben ist.</param>
        /// <param name="value">Wert, der den Filter steuert (und somit bestimmt, ob der Filter angewendet werden soll).</param>
        /// <returns>Das entsprechend gefilterte IQueryable.</returns>
        public static IQueryable<TBo> ApplyOptionalWhereFilter<TBo, TFilterValue>(this IQueryable<TBo> context, Expression<Func<TBo, bool>> predicate, TFilterValue value)
        {
            // Unverändert zurückgeben, falls kein Filter-Wert angegeben ist.
            if (value?.Equals(default(TFilterValue)) ?? true) return context;

            // Ansonsten gemäß der angegebenenen Bedingung filtern.
            return context.Where(predicate);
        }

        private static Expression<Func<TBo, bool>> CreateEqualExpression<TBo, TFilter>(Expression<Func<TBo, TFilter>> target, TFilter filter)
        {
            var typepar = target.Parameters.Single();
            var expr = Expression.Equal(target.Body, Expression.Constant(filter));
            return Expression.Lambda<Func<TBo, bool>>(expr, typepar);
        }
    }
}
