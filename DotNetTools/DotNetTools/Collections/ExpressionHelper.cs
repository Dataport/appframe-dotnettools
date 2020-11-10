using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Dataport.AppFrameDotNet.DotNetTools.Text.Extensions;
using Dataport.AppFrameDotNet.DotNetTools.Validation;
using Dataport.AppFrameDotNet.DotNetTools.Validation.Extensions;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections
{
    /// <summary>
    /// Stellt Methoden zur Verfügung um <see cref="Expression"/>s zu erzeugen.
    /// </summary>
    public static class ExpressionHelper
    {
        /// <summary>
        /// Erzeugt eine Expression für einen Gleichheitsausdruck.
        /// Verhalten: Gleichheit funktioniert mit implementierten Operator oder Referenzgleichheit, nicht mit IComparable.
        /// </summary>
        /// <typeparam name="TInstance">Der Typ der Instanz</typeparam>
        /// <typeparam name="TElement">Der Typ des Elements</typeparam>
        /// <param name="target">Eine Expression, welche einen Wert <typeparamref name="TElement"/> aus der Instanz erzeugt.</param>
        /// <param name="filter">Der Wert mit dem verglichen werden soll.</param>
        /// <returns>Die Gleichheits-Expression.</returns>
        public static Expression<Func<TInstance, bool>> CreateEqualExpression<TInstance, TElement>(Expression<Func<TInstance, TElement>> target, TElement filter)
        {
            // Feature-Request: Support IComparable

            var parameter = target.Parameters.Single();
            var expr = Expression.Equal(target.Body, Expression.Constant(filter));
            return Expression.Lambda<Func<TInstance, bool>>(expr, parameter);
        }

        /// <summary>
        /// Erzeugt eine Expression für String-Like-Suchanfragen.
        /// Verhalten: Wildcards dürfen nur am Anfang und Ende des Filters stehen.
        /// Die Elemente der später verwendeten Collection dürfen nicht <see langword="null"/> sein.
        /// </summary>
        /// <typeparam name="TElement">Der Typ des Elements</typeparam>
        /// <param name="valueSelector">Selektionslogik für das Element des Objekts</param>
        /// <param name="filter">Der Filterausdruck</param>
        /// <param name="wildcard">Die Wildcard im Filterausdruck.</param>
        /// <returns>Die Suchexpression.</returns>
        public static Expression<Func<TElement, bool>> CreateLikeExpression<TElement>(Expression<Func<TElement, string>> valueSelector, string filter, char wildcard = '*')
        {
            Verify.That(valueSelector, nameof(valueSelector)).IsNotNull();

            var methodCalls = GetCompareExpressions(valueSelector, filter.ReplaceRecursive("**", "*"), wildcard);
            Expression calls = methodCalls.First();
            foreach (var methodCall in methodCalls.Skip(1))
            {
                calls = Expression.And(calls, methodCall);
            }

            // verknüpfe mit der Vorbedingung, dass die Werte nicht null sind.
            var notNullCheck = Expression.NotEqual(valueSelector.Body, Expression.Constant(null, typeof(string)));
            var body = Expression.AndAlso(notNullCheck, calls); // And will not work!

            var parameter = valueSelector.Parameters.Single();

            return Expression.Lambda<Func<TElement, bool>>(body, parameter);
        }

        /// <summary>
        /// Konkretisiert den Typen des Parameters der Expression.
        /// </summary>
        /// <typeparam name="TBefore">Typ des Parameters in der ursprünglichen Expression.</typeparam>
        /// <typeparam name="TAfter">Konkreterer Typ für den Parameter der resultierenden Expression.</typeparam>
        /// <typeparam name="TResult">Typ des Rückgabewerts der Expression.</typeparam>
        /// <param name="context">Expression, deren Parametertyp konkretisiert werden soll.</param>
        /// <returns>Expression, die identisch zur ursprünglichen Expression ist, aber einen konkreteren Parametertypen definiert.</returns>
        /// <remarks>Zur Verwendung von allgemein definierten Expressions für konkretere Typen, da die Kovarianz nicht automatisch aufgelöst werden kann.</remarks>
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute", Justification = "An dieser Stelle darf eine Exception auftreten.")]
        public static Expression<Func<TAfter, TResult>> ChangeInputType<TBefore, TAfter, TResult>(Expression<Func<TBefore, TResult>> context)
            where TAfter : TBefore
        {
            // Implementierung orientiert sich an:
            // http://stackoverflow.com/questions/15212779/convert-expressionfunct1-bool-to-expressionfunct2-bool-dynamically

            if (context == null) throw new ArgumentNullException(nameof(context));

            var beforeParameter = context.Parameters.Single();
            var afterParameter = Expression.Parameter(typeof(TAfter), beforeParameter.Name);
            var visitor = new SubstitutionExpressionVisitor(beforeParameter, afterParameter);

            return Expression.Lambda<Func<TAfter, TResult>>(visitor.Visit(context.Body), afterParameter);
        }

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <typeparam name="TOut">Typ der Property, die selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        public static Expression<Func<TInstance, TOut>> SelectPropertyByName<TInstance, TOut>(string propertyName) where TInstance : class
            // Parameter für Expression selbst erzeugen.
            => SelectPropertyByName<TInstance, TOut>(propertyName, Expression.Parameter(typeof(TInstance), "p"));

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert
        /// mit Angabe der zu verwendenden ParameterExpression für den Property-Zugriff.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <typeparam name="TOut">Typ der Property, die selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <param name="parameter">ParameterExpression, über die auf die Property zugegriffen werden soll.</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        /// <exception cref="ArgumentNullException">Wird geworfen, wenn <paramref name="parameter"/> null ist.</exception>
        /// <exception cref="InvalidOperationException">Wird geworfen, wenn der Typ von <paramref name="parameter"/> von <typeparamref name="TInstance"/> abweicht.</exception>
        public static Expression<Func<TInstance, TOut>> SelectPropertyByName<TInstance, TOut>(string propertyName, ParameterExpression parameter) where TInstance : class
            => Expression.Lambda<Func<TInstance, TOut>>(CreatePropertySelectorExpression<TInstance>(propertyName, parameter), parameter);

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        public static LambdaExpression SelectPropertyByNameX<TInstance>(string propertyName) where TInstance : class
            // Parameter für Expression selbst erzeugen.
            => SelectPropertyByNameX<TInstance>(propertyName, Expression.Parameter(typeof(TInstance), "p"));

        /// <summary>
        /// Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert
        /// mit Angabe der zu verwendenden ParameterExpression für den Property-Zugriff.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Entität, deren Property selektiert werden soll.</typeparam>
        /// <param name="propertyName">Name der Property, die durch die Expression selektiert werden soll</param>
        /// <param name="parameter">ParameterExpression, über die auf die Property zugegriffen werden soll.</param>
        /// <returns>Eine Expression, die die Property selektiert</returns>
        public static LambdaExpression SelectPropertyByNameX<TInstance>(string propertyName, ParameterExpression parameter) where TInstance : class
            => Expression.Lambda(CreatePropertySelectorExpression<TInstance>(propertyName, parameter), parameter);

        /// <summary>
        /// Gibt den Namen eines Members zurück, welcher in der Anweisung vorkommt.
        /// Verhalten: Funktioniert auch für Cast-Ausdrücke und Vergleichsausdrücke.
        /// Kommen mehrere Properties in dem Ausdruck vor, wird der Name des Ersten genommen.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Instanz</typeparam>
        /// <param name="expression">Anweisungsvorschrift, wie das Property zu laden ist.</param>
        /// <returns>Der Name des Properties</returns>
        public static string ExtractMemberName<TInstance>(Expression<Func<TInstance, object>> expression) where TInstance : class
        {
            return GetMember(expression)?.Member.Name;
        }

        /// <summary>
        /// Gibt den Namen eines Members zurück, welcher in der Anweisung vorkommt.
        /// Verhalten: Funktioniert auch für Cast-Ausdrücke und Vergleichsausdrücke.
        /// Kommen mehrere Properties in dem Ausdruck vor, wird der Name des Ersten genommen.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Instanz</typeparam>
        /// <typeparam name="TValue">Typ des Wertes</typeparam>
        /// <param name="expression">Anweisungsvorschrift, wie das Property zu laden ist.</param>
        /// <returns>Der Name des Properties</returns>
        public static string ExtractMemberName<TInstance, TValue>(Expression<Func<TInstance, TValue>> expression) where TInstance : class
        {
            return GetMember(expression)?.Member.Name;
        }

        /// <summary>
        /// Gibt das Attribut eines Members zurück, welcher in der Anweisung vorkommt.
        /// Verhalten: Funktioniert auch für Cast-Ausdrücke und Vergleichsausdrücke.
        /// Kommen mehrere Properties in dem Ausdruck vor, wird das Erste genommen.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Instanz</typeparam>
        /// <typeparam name="TAttribute">Typ des Attributs</typeparam>
        /// <param name="expression">Anweisungsvorschrift, wie das Property zu laden ist.</param>
        /// <returns>Das Attribut des Properties</returns>
        public static TAttribute ExtractMemberAttribute<TInstance, TAttribute>(Expression<Func<TInstance, object>> expression)
            where TInstance : class
            where TAttribute : Attribute
        {
            var body = GetMember(expression);

            if (body == null)
            {
                return null;
            }

            // Bei Abgeleiteten Klassen würde GetCustomAttribute nicht die Attribute von TInstance durchsuchen
            // sondern die Attribute der Basis-Klasse.
            return typeof(TInstance).GetMember(body.Member.Name).SingleOrDefault()?.GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        /// Gibt das Attribut eines Members zurück, welcher in der Anweisung vorkommt.
        /// Verhalten: Funktioniert auch für Cast-Ausdrücke und Vergleichsausdrücke.
        /// Kommen mehrere Properties in dem Ausdruck vor, wird das Erste genommen.
        /// </summary>
        /// <typeparam name="TInstance">Typ der Instanz</typeparam>
        /// <typeparam name="TValue">Typ des Wertes</typeparam>
        /// <typeparam name="TAttribute">Typ des Attributs</typeparam>
        /// <param name="expression">Anweisungsvorschrift, wie das Property zu laden ist.</param>
        /// <returns>Das Attribut des Properties</returns>
        public static TAttribute ExtractMemberAttribute<TInstance, TValue, TAttribute>(Expression<Func<TInstance, TValue>> expression)
            where TInstance : class
            where TAttribute : Attribute
        {
            var body = GetMember(expression);

            if (body == null)
            {
                return null;
            }

            // Bei Abgeleiteten Klassen würde GetCustomAttribute nicht die Attribute von TInstance durchsuchen
            // sondern die Attribute der Basis-Klasse.
            return typeof(TInstance).GetMember(body.Member.Name).SingleOrDefault()?.GetCustomAttribute<TAttribute>();
        }

        private static MemberExpression GetMember(LambdaExpression expression)
        {
            if (expression == null)
            {
                return null;
            }

            return expression.Body as MemberExpression
                   ?? GetMember(expression.Body as UnaryExpression)
                   ?? GetMember(expression.Body as BinaryExpression);
        }

        private static MemberExpression GetMember(UnaryExpression expression)
        {
            if (expression == null)
            {
                return null;
            }

            return
                expression.Operand as MemberExpression ??
                GetMember(expression.Operand as UnaryExpression) ??
                GetMember(expression.Operand as BinaryExpression);
        }

        private static MemberExpression GetMember(BinaryExpression expression)
        {
            return expression?.Left as MemberExpression ?? expression?.Right as MemberExpression;
        }

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

        private static List<MethodCallExpression> GetCompareExpressions<TElement>(Expression<Func<TElement, string>> valueSelector, string filter, char wildcard)
        {
            var result = new List<MethodCallExpression>();
            if (filter == null || !filter.Contains(wildcard))
            {
                result.Add(Expression.Call(valueSelector.Body, GetStringMethodInfo("Equals"), Expression.Property(Expression.Constant(new { Value = filter }), "Value")));
                return result;
            }

            int lastOffset = 0;

            // starte von 1, das erste Zeichen ist in jedem Fall egal!
            for (int i = 1; i < filter.Length; i++)
            {
                if (filter[i] != wildcard)
                {
                    continue;
                }

                if (lastOffset == 0 && filter[0] != wildcard)
                {
                    result.Add(Expression.Call(valueSelector.Body, GetStringMethodInfo("StartsWith"), Expression.Property(Expression.Constant(new { Value = filter.Substring(0, i).Trim(wildcard) }), "Value")));
                    lastOffset = i;
                    continue;
                }

                result.Add(Expression.Call(valueSelector.Body, GetStringMethodInfo("Contains"), Expression.Property(Expression.Constant(new { Value = filter.Substring(lastOffset, i - lastOffset).Trim(wildcard) }), "Value")));
                lastOffset = i;
            }

            result.Add(Expression.Call(valueSelector.Body, GetStringMethodInfo("EndsWith"), Expression.Property(Expression.Constant(new { Value = filter.Substring(lastOffset, filter.Length - lastOffset).Trim(wildcard) }), "Value")));

            return result;
        }

        private static MethodInfo GetStringMethodInfo(string name)
        {
            return typeof(string).GetMethod(name, new[] { typeof(string) });
        }

        /// <summary>
        /// ExpressionVisitor, der den Zugriff auf eine bestimmte Expression umleitet.
        /// </summary>
        private class SubstitutionExpressionVisitor : ExpressionVisitor
        {
            private readonly Expression _before, _after;

            public SubstitutionExpressionVisitor(Expression before, Expression after)
            {
                _before = before;
                _after = after;
            }

            public override Expression Visit(Expression node)
            {
                return node == _before ? _after : base.Visit(node);
            }
        }
    }
}