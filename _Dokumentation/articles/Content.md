# Table of Content

---
## Collections

### Klassen

**ExpressionHelper (seit Version 2.3)**

|Methode|Beschreibung|Version|
|---|---|---|
|ChangeInputType|Konkretisiert den Typen des Parameters der Expression bei nicht automatisch erkannter Kovarianz.|2.3|
|CreateEqualExpression|Erzeugt eine Expression für einen Gleichheitsausdruck.|2.3|
|CreateLikeExpression|Erzeugt eine Expression für String-Like-Suchanfragen.|2.3|
|ExtractMemberAttribute|Gibt das Attribut eines Members zurück, welcher in der Anweisung vorkommt.|3.1|
|ExtractMemberName|Gibt den Namen eines Members zurück, welcher in der Anweisung vorkommt.|2.3|
|SelectPropertyByName|Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert.|2.3|
|SelectPropertyByNameX|Erzeugt eine Expression, die die Property mit dem angegebenen Namen selektiert (verschleiert den konkreten Typ im Rückgabeergebnis).|2.3|

**NonRepeatableEnumerable (seit Version 2.1)**

Eine IEnumerable, welche ihren Inhalt nur einmalig enumeriert.

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|Expression|ChangeInputType|Konkretisiert den Typen des Parameters der Expression.|2.3|
|ICollection<T>|AddOrReplace|Ersetzt einen Eintrag in einer Liste durch einen anderen wenn dieser bereits vorhanden ist oder fügt diesen hinzu.|2.1|
|ICollection<T>|AddOrReplaceAll|Ersetzt alle Einträge in einer Liste durch einen anderen wenn dieser bereits vorhanden ist oder fügt diesen hinzu.|2.2|
|IDictionary<K,V>|AddIfMissing **[Obsolete]**|Fügt den Eintrag hinzu, wenn der Schlüssel nicht existiert.|3.0|
|IDictionary<K,V>|GetOrAdd|Gibt den Eintrag im Wörterbuch zurück und legt diesen an, wenn er noch nicht vorhanden ist.|2.1|
|IDictionary<K,V>|TryAdd|Versucht einen Eintrag hinzuzufügen, wenn der Schlüssel nicht existiert.|3.2|
|IEnumerable<T>|Add|Fügt einer Enumeration verschiedene Werte hinzu.|2.1|
|IEnumerable<T>|Append|Fügt einer Enumeration verschiedene Werte hinzu.|2.1|
|IEnumerable<T>|Chunk|Teilt eine Enumerable in verschiedene Chunks mit festgelegter Größe.|2.1|
|IEnumerable<T>|ContainAll|Gibt an, ob die Enumeration alle der übergebenen Kandidaten enthält.|2.1|
|IEnumerable<T>|ContainOneOf|Gibt an, ob die Enumeration einen oder mehrere der übergebenen Kandidaten enthält.|2.1|
|IEnumerable<T>|CrossJoin|Gibt eine Enumeration aller Kombinationen der beiden Enumerationen zurück.|2.1|
|IEnumerable<T>|Distinct|Gibt nur eindeutige Elemente aus Enumerable zurück.|2.1|
|IEnumerable<T>|FirstIndexOf|Gibt den numerischen Index des ersten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.|2.1|
|IEnumerable<T>|ForEach|Führt die angegebene Aktion für jedes Element der Enumeration aus.|2.1|
|IEnumerable<T>|IsEmpty|Gibt an, ob die Enumeration leer ist.|2.1|
|IEnumerable<T>|IsNullOrEmpty|Gibt an, ob die Enumeration null oder leer ist.|2.1|
|IEnumerable<T>|Join|Fügt eine Auflistung von Elementen zu einem string zusammen.|2.1|
|IEnumerable<T>|JoinNotEmpty|Fügt eine Auflistung von Elementen zu einem string zusammen. Dabei werden nur Elemente betrachtet, die nicht null oder leer sind.|2.1|
|IEnumerable<T>|LastIndexOf|Gibt den numerischen Index des letzten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.|2.1|
|IEnumerable<T>|Second|Gibt das zweite Element der Enumeration zurück.|2.1|
|IEnumerable<T>|SecondIndexOf|Gibt den numerischen Index des zweiten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.|2.1|
|IEnumerable<T>|SecondOrDefault|Gibt das zweite Element der Enumeration zurück, oder dessen Defaultwert.|2.1|
|IEnumerable<T>|Third|Gibt das dritte Element der Enumeration zurück|2.1|
|IEnumerable<T>|ThirdIndexOf|Gibt den numerischen Index des dritten Elements in der Auflistung an, welcher die übergebene Bedingung erfüllt.|2.1|
|IEnumerable<T>|ThirdOrDefault|Gibt das dritte Element der Enumeration zurück, oder dessen Defaultwert.|2.1|
|IEnumerable<T>|ToDictionary|Transformiert eine Auflistung in ein Dictionary.|2.1|
|IEnumerable<T>|ToReadOnlyDictionary|Transformiert eine Auflistung in ein schreibgeschütztes Dictionary.|2.1|
|IEnumerable<T>|ToReadOnlyList|Wrappt die übergebene Enumerable in einer schreibgeschützten Liste.|3.1|
|IEnumerable<T>|WhereEquals|Filtert alle Einträge die dem übergebenen Wert entsprechen.|2.3|
|IEnumerable<T>|WhereEqualsOptional|Filtert alle Einträge die dem übergebenen Wert entsprechen. Ist der Filter null, wird nicht gefiltert.|2.3|
|IEnumerable<T>|WhereLike|Filtert alle Einträge die dem Like-Statement entsprechen.|2.3|
|IEnumerable<T>|WhereLikeOptional|Filtert alle Einträge die dem Like-Statement entsprechen. Ist der Filter leer, wird nicht gefiltert.|2.3|
|IEnumerable<T>|WhereNotNull|Filtert die Elemente aus einer Liste, die null sind.|2.3|
|IOrderedEnumerable<T>|Page|Hilfsmethode um große Datenmengen in Pages zerlegt durchzugehen.|2.3|

### Models

**PagedResult (seit Version 2.3)**

Ein Model zum Abbilden aller notwendigen Paging-Resultate.

---
## Comparison

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|class|IsEqualOnPropertyLevel|Vergleicht zwei Objekte anhand der Werte ihrer gemeinsamen Properties.|2.1|
|IComparable|IsInRange|Gibt an ob der Wert innerhalb eines bestimmten Wertebereichs liegt. Bietet die Steuerung von Grenzwerten.|2.1|
|IComparable|IsWithin|Gibt an ob sich der Wert im Rahmen eines Maximalwerts befindet. Bietet die Steuerung von Grenzwerten.|2.1|
|IDictionary|IsEquivalentTo|Gibt an, ob das übergebene Dictionary mit dem bestehenden Dictionary vollständig kongruent ist.|3.1|
|IDictionary|IsSubsetOf|Gibt an, ob das bestehende Dictionary eine Teilmenge des übergebenen Dictionaries ist.|3.1|

### Models

**BoundaryType (seit Version 2.1)**

Enum zum Aussteuerng von Grenzwerten.

**IgnoreInPropertyComparisonAttribute (seit Version 3.0)**

Default IgnoreAttribute für ComparsionExtensions.IsEqualOnPropertyLevel.

**MemberComparisonResult (seit Version 2.1)**

Definiert einen konkreten Unterschied im Vergleich zwischen zwei Objekten.

---
## IO

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|HttpStatusCode|IsSuccessStatusCode|Gibt an, ob der Statuscode einen Erfolg ausdrückt oder nicht.|3.1|
|Stream|ReadAllBytes|Überführt den Stream in ein Byte-Array.|2.3|
|String|AsStream|Überführt einen String in einen Stream.|2.3|

---
## Numeric

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|decimal|AsPercentageOf|Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.|2.1|
|decimal|CountDigits|Zählt die Anzahl der Ziffern innerhalb der Zahl, die vor dem Komma stehen.|2.1|
|decimal|CountDigitsDecimal|Zählt die Anzahl der Dezimalstellen innerhalb der Zahl.|2.1|
|decimal|GetPercentageAmount|Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.|2.1|
|double|AsPercentageOf|Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.|2.1|
|double|CountDigits|Zählt die Anzahl der Ziffern innerhalb der Zahl, die vor dem Komma stehen.|2.1|
|double|CountDigitsDecimal|Zählt die Anzahl der Dezimalstellen innerhalb der Zahl.|2.1|
|double|GetPercentageAmount|Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.|2.1|
|float|AsPercentageOf|Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.|2.1|
|float|CountDigits|Zählt die Anzahl der Ziffern innerhalb der Zahl, die vor dem Komma stehen.|2.1|
|float|CountDigitsDecimal|Zählt die Anzahl der Dezimalstellen innerhalb der Zahl.|2.1|
|float|GetPercentageAmount|Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.|2.1|
|int|AsPercentageOf|Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.|2.1|
|int|AsWords|Transformiert die übergebene Zahl in einen zusammenhängenden deutschen String.|2.2|
|int|CountDigits|Zählt die Anzahl der Ziffern innerhalb der Zahl.|2.1|
|int|GetPercentageAmount|Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.|2.1|
|int|WithLeadingZeros|Fügt einer Zahl führende Nullen hinzu.|2.1|
|long|AsPercentageOf|Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.|2.1|
|long|AsWords|Transformiert die übergebene Zahl in einen zusammenhängenden deutschen String.|2.2|
|long|CountDigits|Zählt die Anzahl der Ziffern innerhalb der Zahl.|2.1|
|long|GetPercentageAmount|Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.|2.1|
|short|AsPercentageOf|Gibt an, wie viel Prozent die übergebene Zahl in Relation zur Gesamtheit darstellt.|2.1|
|short|AsWords|Transformiert die übergebene Zahl in einen zusammenhängenden deutschen String.|2.2|
|short|CountDigits|Zählt die Anzahl der Ziffern innerhalb der Zahl.|2.1|
|short|GetPercentageAmount|Gibt zurück, wie groß ein prozentuale Anteil der übergebenen Zahl ist.|2.1|

### Models

**PercentageType (seit Version 2.1)**

Gibt an, wie eine prozentuale Darstellung erfolgen soll.

---
## Reflection

### Klassen

**EnumHelper (seit Version 2.2)**

|Methode|Beschreibung|Version|
|---|---|---|
|AsDictionary|Überführt das Enum in ein Dictionary.|3.1|
|ChangeType|Parst ein Enum-Value in ein Value eines anderen Typen.|3.1|
|Count|Gibt die Anzahl der Enum-Values zurück.|2.2|
|FitsByIndexInto|Gibt an, ob das Enum mit all seinen Indexen vollständig von einem anderen Enum abgedeckt wird.|3.1|
|FitsByNameInto|Gibt an, ob das Enum mit all seinen Namen vollständig von einem anderen Enum abgedeckt wird.|3.1|
|FitsInto|Gibt an, ob das Enum mit all seinen Index-Wert-Kombinationen vollständig von einem anderes Enum abgedeckt wird.|3.1|
|GetValues|Gibt alle Values des Enums zurück.|2.2|
|GetValuesWhereAttribute|Gibt alle Values des Enums anhand eines Predikats über ein Attribut an den Membern zurück.|2.2|
|IsSubstituteOf|Gibt an, ob das Enum vollständig kongruent zu einem anderen Enum ist.|3.1|

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|Assembly|LoadEmbeddedResource|Lädt den Inhalt einer Datei die als eingebettete Resource im System verfügbar ist.|2.1|
|class|Get|Gibt den angeforderten Wert zurück.|2.1|
|class|MergeWith|Verschmilzt mehrere Propertys zweier Instanzen miteinander.|2.1|
|class|Set|Setzt einen Wert.|2.1|
|Enum|ChangeType|Parst ein Enum-Value in ein Value eines anderen Typen.|3.1|
|IDictionary|DowngradeKeyType|Definiert den Typ der Schlüsselwerte des Dictionaries als unterliegenden Typ in der Vererbungshierarchie um.|3.1|
|IDictionary|DowngradeValueType|Definiert den Typ der Werte des Dictionaries als unterliegenden Typ in der Vererbungshierarchie um.|3.1|
|IDictionary|UpgradeKeyType|Definiert den Typ der Schlüsselwerte des Dictionaries als oberliegenden Typ in der Vererbungshierarchie um.|3.1|
|IDictionary|UpgradeValueType|Definiert den Typ der Werte des Dictionaries als oberliegenden Typ in der Vererbungshierarchie um.|3.1|
|IEnumerable|DowngradeType|Definiert den Typ der Enumerable als unterliegenden Typ in der Vererbungshierarchie um.|3.1|
|IEnumerable|UpgradeType|Definiert den Typ der Enumerable als oberliegenden Typ in der Vererbungshierarchie um.|3.1|
|instance|AsArray|Wrappt das übergebene Objekt in ein Array des Datentyps.|2.3|
|instance|AsArrayOf|Wrappt das übergebene Objekt in ein Array des Zieltyps.|2.3|
|String|ToEnumMember|Konvertiert einen String in einen Member des übergebenen Enum-Typens unter Berücksichtigung eines Attributs über dem Member.|2.2|
|struct|AsNullable|Wrappt das übergebene Objekt in eine Nullable-Struktur.|2.1|
|Type|Extends|Gibt an, dass der Typ von einem anderen Typen ableitet.|2.1|
|Type|GetConstantValue|Gibt einen öffentlich sichtbaren statischen oder konstanten Member-Wert zurück.|3.1|
|Type|GetPropertyByName|Gibt das Property anhand seines Namens zurück.|2.1|
|Type|Implements|Gibt an, dass der Typ einen anderen Typen implementiert.|2.1|
|Type|IsGenericCollection|Gibt an, ob der Typ eine generische ICollection ist oder nicht.|2.1|
|Type|IsGenericEnumerable|Gibt an, ob der Typ eine generische IEnumerable ist oder nicht.|2.1|
|Type|IsGenericType|Gibt an, ob der Typ generisch ist und der übergebenen Definition entspricht, oder nicht.|2.1|
|Type|IsGenericTypeFor|Gibt an, ob der Typ generisches ist und der übergebenen Typen diesen implementiert bzw. von ihm ableitet.|2.1|
|Type|IsGenericTypeOf|Gibt an, ob der Typ den übergebenen generischen Typ implementiert bzw. von ihm ableitet.|2.1|
|Type|IsNullable|Gibt an, ob der Datentyp nullable ist.|2.1|
|Type|IsOrIsInheritFrom|Gibt an, ob der Typ in seiner Vererbungskette den Vergleichstyp enthält.|2.1|

---
## Text

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|String|CleanJson|Bereinigt Texte für Ausgabe in UI-Textbox oder Textdatei.|2.3|
|String|EmptyAsNull|Normalisiert leer Strings auf null/nothing.|2.1|
|String|EscapeXml|Escaped XML-Characters in dem übergebenen String.|2.1|
|String|FirstLetterUpperCase|Setzt das erste Zeichen des übergebenen Strings in UpperCase.|2.2|
|String|FromBase64|Konvertiert einen String in einen Base64-String.|2.1|
|String|ReplaceRecursive|Ersetzt einen Substring innerhalb eines Strings so lange bis der Substring nicht mehr vorhanden ist.|3.1|
|String|ReverseLogical|Invertiert einen String und dreht dabei die Zeichen um, zu denen es ein logisches gespiegeltes Gegenüber gibt.|2.3|
|String|Strip|Entfernt die übergebenen Substrings aus dem String.|2.3|
|String|StripLineFeeds|Ersetzt sämtliche Zeilenumbrüche durch Leerzeichen.|2.3|
|String|StripSurrounding|Entfernt umschließende Zeichenfolgen von einem String.|2.3|
|String|ToBase64|Konvertiert einen Base64-String in einen normalen String.|2.1|
|String|ToEnumMember|Konvertiert einen String in einen Member des übergebenen Enum-Typens.|2.1|
|String|Truncate|Kürzt den String auf eine gegebene Anzahl von Zeichen, so dies notwendigt ist.|2.1|
|String|TryToEnumMember|Versucht einen String in einen Member des übergebenen Enum-Typens zu konvertieren.|2.2|

---
## Time

### Extensions

|Typ|Methode|Beschreibung|Version|
|---|---|---|---|
|DateTime|BeginOfDay|Gibt das Datum mit der min. Uhrzeit zurück (0:00 Uhr).|2.3|
|DateTime|EndOfDay|Gibt das Datum mit der max. Uhrzeit zurück (23:59:59.999).|2.3|
|DateTime|FirstDayOfMonth|Gibt den ersten des Monats zurück in dem sich das Datum befindet.|2.3|
|DateTime|FristDayOfNextMonth|Gibt zum Datum den ersten des nächsten Monats zurück in dem sich das Datum befindet.|2.3|
|DateTime|LastDayOfMonth|Gibt den letzten des Monats zurück in dem sich das Datum befindet.|2.3|
|DateTime|SetDay|Setzt das Datum auf einen bestimmten Tag im Monat fest ohne die anderen Datumskomponenten zu ändern.|2.3|
|DateTime|Truncate|Gibt ein DateTime zurück, bei dem im Vergleich zum eingehenden DateTime informationen weggeschnitten werden.|2.3|

### Models

**DateTimePart (seit Version 2.3)**

Gibt einen Teil von DateTime an.

---
## Validation

### Klassen

**ValidationResults (seit Version 2.1)**

Eine Sammelklasse für Validierungsnachrichten

|Properties|Beschreibung|Version|
|---|---|---|
|Errors|Gibt eine Auflistung aller Fehlermeldungen zurück.|2.1|
|Information|Gibt eine Auflistung aller Informationen zurück.|3.0|
|Severity|Gibt den Schweregrad der Validierung zurück.|2.1|
|Warnings|Gibt eine Auflistung aller Warnungen zurück.|2.1|

|Methode|Beschreibung|Version|
|---|---|---|
|AddError|Fügt eine Fehlermeldung hinzu.|2.1|
|AddInformation|Fügt eine Informationsmeldung hinzu.|2.1|
|AddWarning|Fügt eine Warnungsmeldung hinzu.|2.1|

**Verify (seit Version 2.1)**

Eine Shortcut-Klasse für Prüfungen
```c
// Ohne Verify
if (string.IsNullOrEmpty(str))
{
    throw new ArgumentException("myString");   
}

// Mit Verify
Verify.That(str, "myString").IsNotNullOrEmpty();
```
|Methode|Beschreibung|Version|
|---|---|---|
|AllElementsNotNull|Stellt sicher, dass alle Werte in der übergebenen Collection nicht null sind.|3.1|
|ContainsElements|Stellt sicher, dass eine Enumerable Elemente hat.|2.1|
|HasCount|Stellt sicher, dass die übergebene Collection eine exakte Anzahl an Elementen enthält.|3.1|
|HasMaximumSize|Stellt sicher, dass die übergebene Collection eine Maximalanzahl an Elementen enthält.|3.1|
|HasMinimumSize|Stellt sicher, dass die übergebene Collection eine Mindestanzahl an Elementen enthält.|3.1|
|Is|Stellt sicher, dass das Element die übergebene Bedinung erfüllt.|2.1|
|IsDefined|Stellt sicher, dass ein Enum-Member definiert ist.|2.1|
|IsEqualTo|Stellt sicher, dass die Instanz vergleichbar zu einem Vergleichswert ist|2.3|
|IsNotNull|Stellt sicher, dass das Element nicht null ist.|2.1|
|IsNotNullOrEmpty|Stellt sicher, dass eine Enumerable nicht null oder leer ist.|2.1|
|IsNotNullOrWhitespace|Stellt sicher, dass ein string nicht null oder leer ist oder nur Leerzeichen enthält.|2.1|