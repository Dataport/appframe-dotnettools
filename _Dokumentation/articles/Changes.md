# Version 3.2

* Collections
    * ExpressionHelper
        * CreateLikeExpression [Upgrade]
            * Die Wildcard kann nun an beliebiger Position eingesetzt werden
    * ICollection.AddOrReplace [Upgrade]
        * Wird versucht, einen vorhandenen, vergleichbaren Wert mit sich selbst zu ersetzen, tue nichts.
    * ICollection.AddOrReplaceAll
        * [Fix] Die Überladung für IComparable mit einem oldValue, prüft nicht mehr mit Equals sondern mit IComparable.CompareTo
        * [Upgrade] Wird versucht, einen vorhandenen, vergleichbaren Wert mit sich selbst zu ersetzen, erhalte diesen Wert an seiner Position.
    * IDictionary.AddIfMissing [Obsolet]
        * Kann mit TryAdd abgebildet werden und wird in Version 4 entfernt
    * IDictionary.TryAdd [New]
    * IEnumerable.Add [Upgrade]
        * Wenn es sich um eine IList variabler Länge handelt, wird die gleiche Instanz bearbeitet, die hinein gegeben wird.
    * IEnumerable.WhereLike [Upgrade]
        * Die Wildcard kann nun an beliebiger Position eingesetzt werden
    * IEnumerable.WhereLikeOptional [Upgrade]
        * Die Wildcard kann nun an beliebiger Position eingesetzt werden
    * IQueryable.WhereEquals [Upgrade]
        * Eigene Überladungen
    * IQueryable.WhereEqualsOptional [Upgrade]
        * Eigene Überladungen
    * IQueryable.WhereLike [Upgrade]
        * Eigene Überladungen
    * IQueryable.WhereLikeOptional [Upgrade]
        * Eigene Überladungen
* Validation
    * Verify.That().[diverse Methoden] [Fix]
        * ArgumentNullExceptions enthalten immer den festgelegten Namen
        * ArgumentExceptions mit definiertem Text enthalten immer als zweiten Parameter den festgelegten Namen
---

# Version 3.1

* Collections
    * ExpressionHelper
        * CreateLikeExpression [Fix]
            * Like-Statements können nun auch verwendet werden, wenn die gefilterte Collection Null-Werte enthält
        * ExtractMemberAttribute [New]
        * ExtractMemberName [Upgrade]
            * Überladung um den Returntype der Expression detaillierter anzugeben
    * IEnumerable.ToReadOnlyList [New]
    * IEnumerable.WhereLike [Fix]
        * Like-Statements können nun auch verwendet werden, wenn die gefilterte Collection Null-Werte enthält
    * IEnumerable.WhereLikeOptional [Fix]
        * Like-Statements können nun auch verwendet werden, wenn die gefilterte Collection Null-Werte enthält
* Comparison
    * IDictionary.IsEquivalentTo [New]
    * IDictionary.IsSubsetOf [New]
* IO
    * HttpStatusCode.IsSuccessStatusCode [New]
* Reflection
    * IDictionary.DowngradeKeyType [New]
    * IDictionary.DowngradeValueType [New]
    * IDictionary.UpgradeKeyType [New]
    * IDictionary.UpgradeValueType [New]
    * IEnumerable.DowngradeType [New]
    * IEnumerable.UpgradeType [New]
    * Enum.ChangeType [New]
    * EnumHelper
        * AsDictionary [New]
        * ChangeType [New]
        * FitsByIndexInto [New]
        * FitsByNameInto [New]
        * FitsInto [New]
        * IsSubstituteOf [New]
    * Type.GetConstantValue [New]
* Text
    * string.ReplaceRecursive [New]
    * string.StripLineFeeds [Fix]
        * ersetzt durch mehrere LineFeeds hintereinander entstandene multiple Leerzeichen mit einem einzelnen Leerzeichen
* Validation
    * Verify.That(IEnumerable).AllElementsNotNull [New]
    * Verify.That(IEnumerable).HasCount [New]
    * Verify.That(IEnumerable).HasMaximumSize [New]
    * Verify.That(IEnumerable).HasMinimumSize [New]
---

# Ältere Versionen

Es wird empfohlen, beim Einsatz der DotNetTools mindestens Version 3.0 zu benutzen.

Für eine Übersicht, welche Funktionalitäten in welcher Version hinzugekommen sind, siehe auch die Content-Übersicht.