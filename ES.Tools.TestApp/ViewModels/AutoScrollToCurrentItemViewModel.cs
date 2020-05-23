using System.Collections.ObjectModel;
using System.Windows.Input;
using ES.Tools.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public class AutoScrollToCurrentItemViewModel : ViewModel
  {
    private string _selectedItem;

    public AutoScrollToCurrentItemViewModel()
    {
      Items = new ObservableCollection<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
      SelectNextItemCommand = new ActionCommand(SelectNextItemCommandExecute, () => SelectNextItemCommandCanExecute);
    }

    public ObservableCollection<string> Items { get; }

    public string SelectedItem
    {
      get => _selectedItem;
      set
      {
        if (_selectedItem != value)
        {
          _selectedItem = value;
          OnPropertyChanged();
        }
      }
    }

    public ICommand SelectNextItemCommand { get; }

    private bool SelectNextItemCommandCanExecute => Items.Count > 0;

    private void SelectNextItemCommandExecute()
    {
      int idx = 0;

      if (SelectedItem != null)
      {
        idx = Items.IndexOf(SelectedItem);
        if (idx == Items.Count - 1)
        {
          idx = 0;
        }
        else
        {
          idx++;
        }
      }

      SelectedItem = Items[idx];
    }
  }
}