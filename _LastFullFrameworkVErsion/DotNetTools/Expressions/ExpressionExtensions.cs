using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dataport.AppFrameDotNet.DotNetTools.Expressions
{
    /// <summary>
    /// Extension-Methoden zum leichteren Umgang mit Expressions.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Konkretisiert den Typen des Parameters der Expression.
        /// </summary>
        /// <typeparam name="TBefore">Typ des Parameters in der ursprünglichen Expression.</typeparam>
        /// <typeparam name="TAfter">Konkreterer Typ für den Parameter der resultierenden Expression.</typeparam>
        /// <typeparam name="TResult">Typ des Rückgabewerts der Expression.</typeparam>
        /// <param name="context">Expression, deren Parametertyp konkretisiert werden soll.</param>
        /// <returns>Expression, die identisch zur ursprünglichen Expression ist, aber einen konkreteren Parametertypen definiert.</returns>
        /// <remarks>Zur Verwendung von allgemein definierten Expressions für konkretere Typen, da die Kovarianz nicht automatisch aufgelöst werden kann.</remarks>
        public static Expression<Func<TAfter, TResult>> ChangeInputType<TBefore, TAfter, TResult>(this Expression<Func<TBefore, TResult>> context)
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