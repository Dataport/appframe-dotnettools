using System;

namespace Dataport.AppFrameDotNet.DotNetTools.Comparison.Model
{
    /// <summary>
    /// Default IgnoreAttribute für ComparsionExtensions.IsEqualOnPropertyLevel.
    /// </summary>
    /// <remarks>Muss als Parameter dort angegeben werden, wenn es benutzt werden soll.</remarks>
    public class IgnoreInPropertyComparisonAttribute : Attribute
    { }
}