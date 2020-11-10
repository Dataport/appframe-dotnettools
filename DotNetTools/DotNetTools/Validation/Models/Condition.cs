namespace Dataport.AppFrameDotNet.DotNetTools.Validation.Models
{
    /// <summary>
    /// Ein Model mit allen Eigenschaften für eine Validierung
    /// </summary>
    public class Condition<T>
    {
        /// <summary>
        /// Gibt den Namen des Objekts zurück, das überprüft wird.
        /// </summary>
        /// <value>
        /// Der Name des Objekts.
        /// </value>
        public string NameOfObject { get; }

        /// <summary>
        /// Gibt das Objekt zurück, das überprüft wird.
        /// </summary>
        /// <value>
        /// Das Objekt
        /// </value>
        public T ObjectToVerify { get; }

        /// <summary>
        /// Initiiert eine neue Instanz der <see cref="Condition{T}"/> Klasse.
        /// </summary>
        /// <param name="objectToVerify">Das Objekt das geprüft werden soll</param>
        /// <param name="nameOfObject">Der Name des Objekts</param>
        public Condition(T objectToVerify, string nameOfObject)
        {
            ObjectToVerify = objectToVerify;
            NameOfObject = nameOfObject;
        }
    }
}