using System.Collections;
using System.ComponentModel;
using System.Windows.Data;

namespace ES.Tools.Core.MVVM
{
  public static class ViewModelExtensions
  {
    /// <summary>
    /// Returns the default collection view of a list object.
    /// </summary>
    public static CollectionView GetView(this IEnumerable source)
    {
      if (CollectionViewSource.GetDefaultView(source) is CollectionView view)
      {
        if (view is IEditableCollectionView editableCollectionView)
        {
          if (editableCollectionView.IsAddingNew || editableCollectionView.IsEditingItem)
          {
            editableCollectionView.CommitEdit();
          }
        }

        return view;
      }

      return null;
    }

    /// <summary>
    /// Adds a <see cref="SortDescription"/> to the default view of a collection.
    /// </summary>
    public static void AddSorting(this IEnumerable source, params SortDescription[] sortDescriptions)
    {
      var view = source.GetView();
      if (view != null)
      {
        using (view.DeferRefresh())
        {
          foreach (var description in sortDescriptions)
          {
            view.SortDescriptions.Add(description);
          }
        }
      }
    }

    /// <summary>
    /// Sets a new <see cref="SortDescription"/> to the default view of a collection.
    /// </summary>
    public static void SetSorting(this IEnumerable source, params SortDescription[] sortDescriptions)
    {
      var view = source.GetView();

      if (view != null)
      {
        using (view.DeferRefresh())
        {
          view.SortDescriptions.Clear();

          foreach (var description in sortDescriptions)
          {
            view.SortDescriptions.Add(description);
          }
        }
      }
    }

    /// <summary>
    /// Removes all <see cref="SortDescription"/>s from the default view of a collection.
    /// </summary>
    public static void ClearSorting(this IEnumerable source)
    {
      var view = source.GetView();
      if (view != null)
      {
        view.SortDescriptions.Clear();
      }
    }
  }
}