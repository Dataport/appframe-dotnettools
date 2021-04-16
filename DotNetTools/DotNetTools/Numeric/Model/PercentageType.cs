namespace Dataport.AppFrameDotNet.DotNetTools.Numeric.Model
{
    // Test für Anpassung an der automatischen GitHub-Synchronisierung

    /// <summary>
    /// Gibt an, wie eine prozentuale Darstellung erfolgen soll.
    /// </summary>
    public enum PercentageType
    {
        /// <summary>
        /// Die Darstellung erfolgt auf mathematisch korrektem Weg, d.h. 20% = 0.2
        /// </summary>
        Arithmetic,

        /// <summary>
        /// Die Darstellung erfolgt auf leserlichem Weg, d.h. 20% = 20
        /// </summary>
        HumanReadable
    }
}
