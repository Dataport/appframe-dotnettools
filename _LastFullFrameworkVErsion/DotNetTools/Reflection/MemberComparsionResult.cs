using System.Reflection;

namespace Dataport.AppFrameDotNet.DotNetTools.Reflection
{
    /// <summary>
    /// Ergebnis von ObjectComparsionExtensions.IsEqualOnPropertyLevel. 
    /// </summary>
    public struct MemberComparisonResult
    {
        internal MemberComparisonResult(string memberName, MemberInfo sourceMember, MemberInfo targetMember, object sourceValue, object targetValue)
        {
            this.MemberName = memberName;
            this.SourceMember = sourceMember;
            this.TargetMember = targetMember;
            this.SourceValue = sourceValue;
            this.TargetValue = targetValue;
        }

        /// <summary>
        /// Feldname.
        /// </summary>
        public string MemberName { get; set; }

        /// <summary>
        /// Felddefinition aus Quellobjekt.
        /// </summary>
        public MemberInfo SourceMember { get; set; }

        /// <summary>
        /// Quellinformation aus Zielobjekt.
        /// </summary>
        public MemberInfo TargetMember { get; set; }

        /// <summary>
        /// Wert aus Quellobjekt.
        /// </summary>
        public object SourceValue { get; set; }

        /// <summary>
        /// Wert aus Zielobjekt.
        /// </summary>
        public object TargetValue { get; set; }

    }
}
