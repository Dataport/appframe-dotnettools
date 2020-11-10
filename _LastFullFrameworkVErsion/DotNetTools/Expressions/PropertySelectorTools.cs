using System;
using System.Linq.Expressions;

namespace Dataport.AppFrameDotNet.DotNetTools.Expressions
{
    /// <summary>
    /// Hilfsmethoden für den Umgang mit Expressions zum Selektieren von Properties.
    /// </summary>
    public static class PropertySelectorTools
    {
        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert.
        /// </summary>
        /// <typeparam name="TSource">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <typeparam name="TOut">Typ der Property, die selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        public static Expression<Func<TSource, TOut>> SelectPropertyByPath<TSource, TOut>(string propertyName)
            // Parameter für Expression selbst erzeugen.
            => SelectPropertyByPath<TSource, TOut>(propertyName, Expression.Parameter(typeof(TSource), "p"));

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert
        /// mit Angabe der zu verwendenden ParameterExpression für den Property-Zugriff.
        /// </summary>
        /// <typeparam name="TSource">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <typeparam name="TOut">Typ der Property, die selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <param name="parameter">ParameterExpression, über die auf die Property zugegriffen werden soll.</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        /// <exception cref="ArgumentNullException">Wird geworfen, wenn <paramref name="parameter"/> null ist.</exception>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Typ von <paramref name="parameter"/> von <typeparamref name="TSource"/> abweicht.</exception>
        public static Expression<Func<TSource, TOut>> SelectPropertyByPath<TSource, TOut>(string propertyName, ParameterExpression parameter)
            => Expression.Lambda<Func<TSource, TOut>>(CreatePropertySelectorExpression<TSource>(propertyName, parameter), parameter);

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert.
        /// </summary>
        /// <typeparam name="TSource">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <typeparam name="TOut">Typ der Property, die selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        public static Expression SelectPropertyByPathX<TSource, TOut>(string propertyName)
            // Parameter für Expression selbst erzeugen.
            => SelectPropertyByPathX<TSource, TOut>(propertyName, Expression.Parameter(typeof(TSource), "p"));

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert
        /// mit Angabe der zu verwendenden ParameterExpression für den Property-Zugriff.
        /// </summary>
        /// <typeparam name="TSource">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <typeparam name="TOut">Typ der Property, die selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <param name="parameter">ParameterExpression, über die auf die Property zugegriffen werden soll.</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        public static Expression SelectPropertyByPathX<TSource, TOut>(string propertyName, ParameterExpression parameter)
            => Expression.Lambda(CreatePropertySelectorExpression<TSource>(propertyName, parameter), parameter);


        private static Expression CreatePropertySelectorExpression<TSource>(string propertyName, ParameterExpression parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            if (parameter.Type != typeof(TSource)) throw new InvalidOperationException($"Abweichender Typ in explizit angegebener ParameterExpression. { typeof(TSource) } erwartet; ist: { parameter.Type }");

            // Erzeugt eine Expression, um nach einer Property der Entitäten zu sortieren, anhand des Property Namens.
            // http://stackoverflow.com/questions/16013807/unable-to-sort-with-property-name-in-linq-orderby
            Expression body = parameter;

            // Unterstütze hierarchische Property-Navigationen, z.B. "Schueler.Vorname".
            foreach (var singlePropertyName in propertyName.Split('.'))
            {
                body = Expression.Property(body, singlePropertyName);
            }

            return body;
        }
    }
}