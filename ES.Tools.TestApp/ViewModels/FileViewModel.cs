using System;
using System.IO;
using System.Linq;
using ES.Tools.Behaviors;
using ES.Tools.Core.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public class FileViewModel : ViewModel, IFileDragDropTarget
  {
    private string _path;

    public string FilePath
    {
      get => _path;
      set
      {
        if (_path != value)
        {
          _path = value;
          OnPropertyChanged();
        }
      }
    }

    public bool AllowDrop(string[] filePaths)
    {
      return filePaths.Length > 0 && filePaths.Any(x => string.Equals(Path.GetExtension(x), ".txt", StringComparison.InvariantCultureIgnoreCase));
    }

    public void OnFileDrop(string[] filePaths)
    {
      FilePath = filePaths.First(x => string.Equals(Path.GetExtension(x), ".txt", StringComparison.InvariantCultureIgnoreCase));
    }
  }
}
