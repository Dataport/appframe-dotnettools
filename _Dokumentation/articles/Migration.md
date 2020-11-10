# Migrationsanleitung von Version 2.x zu Version 3

## Gelöschte Klassen
Beinhaltende Methoden siehe Kapitel Gelöschte Methoden und Changing Namespaces.

|Namespace|Klassenname|Grund|
|---|---|---|
|Collections|ListExtensions|Falscher Namespace|
|Expression|ExpressionExtensions|Falscher Namespace. Der Expression-Namespace wurde in Collections-Namespace überführt.|
|Expression|PropertySelectorTools|Der Expression-Namespace wurde in Collections-Namespace überführt. Die Funktionalität wurde mit andern Methoden rund um Expressions zusammengelegt.|
|Linq|FilterExtensions|Der Linq-Namespace wurde in Collections-Namespace überführt.|
|Linq|PagingExtensions|Der Linq-Namespace wurde in Collections-Namespace überführt.|
|Linq|WhereClauseExpressions|Der Linq-Namespace wurde in Collections-Namespace überführt. File-Class-Missmatch.|
|Reflection|EmbeddedResourcesTools|Beinhaltende Methoden wurden Extension-Methoden|
|Reflection|ObjectComparsionExtensions|Vergleichsmethoden verschoben in Namespace Comparison, Typo in Klassenname|
|Reflection|ObjectHelper|Methoden drehten sich um Expressions, daher in Collections/ExpressionHelper aufgegangen.|
|Root|DateExtensions|Falscher Namespace|
|Root|EnumExtensions|Falscher Namespace|
|Root|IntegerExtensions|Falscher Namespace|
|Root|ObjectExtensions|Falscher Namespace|
|Root|StreamExtensions|Falscher Namespace|
|Root|StringExtensions|Falscher Namespace|

## Gelöschte Methoden
|Methode|Grund|Migration|Zusatzinfos|
|---|---|---|---|
|DateTime.Truncate(TimeSpan)|Hohe Risiko zu missbräuchlicher Verwendung.|DateTime.Truncate(DateTimePart)||
|DateExtensions.AsNullable(DateTime)|Nicht-Extensionmethode in Extension-Klasse. Zu spezifisch auf DateTime.|DateTime.AsNullable|Neue Methode für alle Structs|
|EmbeddedResourcesTools.GetEmbeddedTextFile()|Methode wurde Extensionmethode, hatte zudem diverse Probleme, Name unglücklich gewählt|Assembly.LoadEmbeddedResource||
|EnumExtensions.GetEnumValues<T>|Klasse wurde aufgelöst, Multiple "Enum" in Name|EnumHelper<T>.GetValues||
|instance.AsArray()|Klasse wurde aufgelöst.|instance.AsArray(true)||
|instance.AsArrayOf()|Klasse wurde aufgelöst.|instance.AsArray(true)||
|instance.CheckIsNotNull()|Klasse wurde aufgelöst.|Verify.That(context, "context").IsNotNull||
|instance.NullSafeEquals()|Klasse wurde aufgelöst.|Verify.That(context, "context").IsEqualTo||
|IEnmerable<string>.StringConcat|Name irreführend, Klasse wurde aufgelöst|IEnumerable<string>.Join||
|IEnmerable<string>.StringConcatNotEmpty|Name irreführend, Klasse wurde aufgelöst|IEnumerable<string>.JoinNotEmpty||
|IQueryable<T>.ApplyOptionalEqualityFilter|Klasse wurde aufgelöst. Name zu sperrig. Zu spezifisch auf IQueryable<T>|IQueryable<T>.WhereEqualsOptional|Neue Methode verarbeitet IEnumerable<T>|
|IQueryable<T>.ApplyOptionalWhereLikeFilter|Klasse wurde aufgelöst. Name zu sperrig. Zu spezifisch auf IQueryable<T>|IQueryable<T>.WhereLikeOptional|Neue Methode verarbeitet IEnumerable<T>|
|IQueryable<T>.ApplyOptionalWhereFilter|Methode war absolut nicht tragbar für ein zentrales Paket.|-||
|ObjectHelper.PropertyName<T>|Klasse wurde aufgelöst, Name schlecht gewählt|ExpressionHelper.ExtractMemberName<T>|Neues class-Constrain für TInstance|
|PropertySelectiorTools.SelectPropertyByPath|Klasse wurde aufgelöst.|ExpressionHelper.SelectPropertyByPath||
|PropertySelectiorTools.SelectPropertyByPathX|Klasse wurde aufgelöst.|ExpressionHelper.SelectPropertyByPathX|Unbenutzter Typparameter TOut entfernt.|
|string.AsCleanDisplayText|Klasse wurde aufgelöst.|string.CleanJson||
|string.NoReturnOrLineFeeds|Methode führte in vielen UseCases zu Problemen|string.StripLineFeeds||
|StringExtensions.EmptyAsNull|Nicht-Extensionmethod in Extensionklasse|string.EmptyAsNull||
|WhereClauseExpressions.Like|Klasse wurde aufgelöst. Nicht-Extensionmethode in Extension-Klasse.|ExpressionHelper.CreateLikeExpression|null als Value ist zulässig, neue Exception bei Wildcard irgendwo in der Mitte|

## Changing Namespaces
|Methode|Alter Namespace|Neuer Namespace|Zusatzinfos|
|---|---|---|---|
|DateTime.BeginOfDay|Root|Time/Extensions||
|DateTime.EndOfDay|Root|Time/Extensions||
|DateTime.FirstDayOfMonth|Root|Time/Extensions||
|DateTime.FirstDayOfNextMonth|Root|Time/Extensions||
|DateTime.LastDayOfMonth|Root|Time/Extensions||
|DateTime.SetDay|Root|Time/Extensions||
|Expression.ChangeInputType|Expressions|Collection/Extensions||
|IEnumerable.WhereNotNull|Linq|Collection/Extensions||
|IgnoreInPropertyComparisonAttribute|Reflection|Comparison/Model|Typo in Attributname gefixt.|
|instance.IsEqualOnPropertyLevel|Reflection|Comparison/Extensions|Neues class-Constraint für die Instanzen|
|instance.MergeWith|Reflection|Reflection/Extensions|Neues class-Constraint für die Instanzen|
|int.AsNullable|Root|Numeric/Extensions|Neue Methode verarbeitet alle Structs|
|IOrderedEnumerable<T>.Page|Linq|Collection/Extensions||
|IOrderedQueryable<T>.Page|Linq|Collection/Extensions||
|IQueryable<T>.WhereLike|Linq|Collection/Extensions|Neue Methode verarbeite IEnumerable<T>, null als Value ist zulässig, neue Exception bei Wildcard irgendwo in der Mitte.|
|List<T>.AddOrReplace|Collections|Collections/Extensions|Die neue Methode verarbeite ICollection<T>|
|MemberComparisonResult|Reflection|Comparison/Model|Setter entfernt, Objekt immutable|
|PagedResult|Linq|Collections/Model|Setter entfernt, public Konstruktor hinzugefügt.|
|Stream.ReadAllBytes|Root|IO/Extensions|Besseres Handling und diverse Bugfixes in der Methode|
|string.Truncate|Root|Text/Extensions||

## Fix Typos
|Namespace|Typ|Alter Name|Neuer Name|
|---|---|---|---|
|Comparison.Model|Klasse|IgnoreInPropertyComparsionAttribute|IgnoreInPropertyComparisonAttribute|
|Numeric.Extensions|Klasse|Formating|Formatting|
|Reflection.Validation|Member|ValidationResults.Informations|ValidationResults.Information|

## Weiteres
* Verify.That(...).IsNotNullOrEmpty wirft im NULL-Fall nun eine ArgumentNullException statt eine ArgumentException.
* ExpressionHelper.SelectPropertyByName(X) hat nun ein class-Constraint
* Dictionary.AddIfMissing funktioniert nun auch für IDictionary