using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Comparison.Model
{
    /// <summary>
    /// Definiert einen konkreten Unterschied im Vergleich zwischen zwei Objekten.
    /// </summary>
    public readonly struct MemberComparisonResult
    {
        internal MemberComparisonResult(string memberName, MemberInfo sourceMember, MemberInfo targetMember, object sourceValue, object targetValue)
        {
            MemberName = memberName;
            SourceMember = sourceMember;
            TargetMember = targetMember;
            SourceValue = sourceValue;
            TargetValue = targetValue;
        }

        /// <summary>
        /// Feldname.
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// Felddefinition aus Quellobjekt.
        /// </summary>
        public MemberInfo SourceMember { get; }

        /// <summary>
        /// Quellinformation aus Zielobjekt.
        /// </summary>
        public MemberInfo TargetMember { get; }

        /// <summary>
        /// Wert aus Quellobjekt.
        /// </summary>
        public object SourceValue { get; }

        /// <summary>
        /// Wert aus Zielobjekt.
        /// </summary>
        public object TargetValue { get; }
    }
}