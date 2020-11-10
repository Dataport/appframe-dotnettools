using System;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection
{
    /// <summary>
    /// Default IgnoreAttribute für ObjectComparsionExtensions.IsEqualOnPropertyLevel.
    /// </summary>
    /// <remarks>Muss als Parameter dort angegeben werden, wenn es benutzt werden soll.</remarks>
    public class IgnoreInPropertyComparsionAttribute : Attribute
    {  }
}
