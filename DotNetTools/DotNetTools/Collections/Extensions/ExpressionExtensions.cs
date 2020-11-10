using System;
using System.Linq.Expressions;

namespace Dataport.AppFrameDotNet.DotNetTools.Collections.Extensions
{
    /// <summary>
    /// Stellt Erweiterungsmethoden für <see cref="Expression"/> zur Verfügung.
    /// </summary>
    public static class ExpressionExtensions
    {
        /// <summary>
        /// Konkretisiert den Typen des Parameters der Expression.
        /// </summary>
        /// <typeparam name="TBefore">Typ des Parameters in der ursprünglichen Expression.</typeparam>
        /// <typeparam name="TAfter">Konkreterer Typ für den Parameter der resultierenden Expression.</typeparam>
        /// <typeparam name="TResult">Typ des Rückgabewerts der Expression.</typeparam>
        /// <param name="expression">Expression, deren Parametertyp konkretisiert werden soll.</param>
        /// <returns>Expression, die identisch zur ursprünglichen Expression ist, aber einen konkreteren Parametertypen definiert.</returns>
        /// <remarks>Zur Verwendung von allgemein definierten Expressions für konkretere Typen, da die Kovarianz nicht automatisch aufgelöst werden kann.</remarks>
        public static Expression<Func<TAfter, TResult>> ChangeInputType<TBefore, TAfter, TResult>(this Expression<Func<TBefore, TResult>> expression)
            where TAfter : TBefore
        {
            return ExpressionHelper.ChangeInputType<TBefore, TAfter, TResult>(expression);
        }
    }
}