# ViewModelExtensions
Namespace: **ES.Tools.MVVM** *(ES.Tools.Core)*

Helpful extension methods that can be use in the context of *ViewModels*.

### Methods

**CollectionView GetView(this IEnumerable source)**

Returns the default collection view of a list object

**AddSorting(this IEnumerable source, params SortDescription[] sortDescriptions)**

Adds a *SortDescription* to the default view of a collection.

**SetSorting(this IEnumerable source, params SortDescription[] sortDescriptions)**

Sets a new *SortDescription* to the default view of a collection.

**ClearSorting(this IEnumerable source)**

Removes all *SortDescription*s from the default view of a collection.

