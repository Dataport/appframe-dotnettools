using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Linq
{
    /// <summary>
    /// Vordefinierte Expresssions für Linq bzw. Linq-To-Entities.
    /// </summary>
    /// <remarks></remarks>
    public static class WhereClauseExpressions
    {

        /// <summary>
        /// Erweiterung für Linq-Ausdrücke, die den definierten Platzhalter durch entsprechende Methoden StartsWith, Contains bzw. EndsWith ersetzt.
        /// *Content* -> Contains
        /// *Content -> EndsWith
        /// Content* -> StartsWith
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="valueSelector"></param>
        /// <param name="value"></param>
        /// <param name="wildcard"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IQueryable<TSource> WhereLike<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, string>> valueSelector, string value, char wildcard = '*')
        {
            return source.Where(Like(valueSelector, value, wildcard));
        }

        /// <summary>
        /// Erweiterung für Linq-Ausdrücke, die den definierten Platzhalter durch entsprechende Methoden StartsWith, Contains bzw. EndsWith ersetzt.
        /// *Content* -> Contains
        /// *Content -> EndsWith
        /// Content* -> StartsWith
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="valueSelector"></param>
        /// <param name="value"></param>
        /// <param name="wildcard"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static Expression<Func<TElement, bool>> Like<TElement>(Expression<Func<TElement, string>> valueSelector, string value, char wildcard)
        {
            if (valueSelector == null)
                throw new ArgumentNullException(nameof(valueSelector));
            if (value == null)
                value = "";

            var method = GetLikeMethod(value, wildcard);

            value = value.Trim(wildcard);

            // Expression.Constant in Expression.Property verpacken.
            // Dadurch kann die von EF kompilierte Query wieder verwendet werden, weil der Wert als Parameter kompiliert wird.
            // Ohne Expression.Property wird der Wert als Konstante in die SQL Query kompiliert, wodurch diese nicht wiederverwendbar ist.
            // http://stackoverflow.com/questions/17569335/create-an-expression-tree-that-generates-a-parametric-query-for-entity-framework
            var body = Expression.Call(valueSelector.Body, method, Expression.Property(Expression.Constant(new { Value = value }), "Value"));

            var parameter = valueSelector.Parameters.Single();

            return Expression.Lambda<Func<TElement, bool>>(body, parameter);
        }


        private static MethodInfo GetLikeMethod(string value, char wildcard)
        {
            //Fall 1: Wildcard nicht enthalten => Kein Like
            if (!value.Contains(wildcard))
            {
                return GeMethodInfo("Equals");
            }

            //Fall 2: Ended mit Wildcard aber startet nicht
            if (value.EndsWith(wildcard.ToString()) & !value.StartsWith(wildcard.ToString()))
            {
                return GeMethodInfo("StartsWith");
            }

            //Fall 3: Startet mit Wildcard aber ende nicht
            if (value.StartsWith(wildcard.ToString()) & !value.EndsWith(wildcard.ToString()))
            {
                return GeMethodInfo("EndsWith");
            }

            //Jetzt kann es nur noch Contains sein
            return GeMethodInfo("Contains");
        }

        private static MethodInfo GeMethodInfo(string name)
        {
            return typeof(string).GetMethod(name, new System.Type[] { typeof(string) });
        }

        /// <summary>
        /// Filter Null/Nothing-Elemente aus einer Enumeration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">Enumeration.</param>
        /// <returns>Gefilterte Enumeration.</returns>
        /// <remarks></remarks>
        /// <exception cref="ArgumentNullException"></exception> 
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return context.Where(x => x != null);
        }
    }
}
