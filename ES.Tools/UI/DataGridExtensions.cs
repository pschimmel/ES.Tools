using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ES.Tools.UI
{
  /// <summary>
  /// Extension methods for <see cref="DataGrid"/>s.
  /// </summary>
  public static class DataGridExtensions
  {
    /// <summary>
    /// Gets a <see cref="DataGridRow"/> that contains a certain item.
    /// </summary>
    public static DataGridRow GetRow(this DataGrid grid, object item)
    {
      if (item == null)
      {
        throw new ArgumentNullException(nameof(item));
      }

      var row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(item);
      if (row == null)
      {
        // May be virtualized, bring into view and try again.
        grid.UpdateLayout();
        grid.ScrollIntoView(item);
        row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromItem(item);
      }
      return row;
    }

    /// <summary>
    /// Gets a <see cref="DataGridRow"/> from its index.
    /// </summary>
    public static DataGridRow GetRow(this DataGrid grid, int rowIndex)
    {
      if (rowIndex < 0 || rowIndex > grid.Items.Count - 1)
      {
        throw new IndexOutOfRangeException("Row index is out of range.");
      }

      var row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
      if (row == null)
      {
        // May be virtualized, bring into view and try again.
        grid.UpdateLayout();
        grid.ScrollIntoView(grid.Items[rowIndex]);
        row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
      }
      return row;
    }

    /// <summary>
    /// Gets a <see cref="DataGridCell"/> from its <see cref="DataGridRow"/> and the column index.
    /// </summary>
    public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int columnIndex)
    {
      var presenter = row.GetVisualChild<DataGridCellsPresenter>();
      if (presenter == null)
      {
        grid.ScrollIntoView(row, grid.Columns[columnIndex]);
        presenter = row.GetVisualChild<DataGridCellsPresenter>();
      }

      if (presenter != null)
      {
        var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
        return cell;
      }

      return null;
    }

    /// <summary>
    /// Gets a <see cref="DataGridCell"/> from its row and column index.
    /// </summary>
    public static DataGridCell GetCell(this DataGrid grid, int rowIndex, int columnIndex)
    {
      var gridRow = GetRow(grid, rowIndex);
      return gridRow == null ? null : GetCell(grid, gridRow, columnIndex);
    }
  }
}
