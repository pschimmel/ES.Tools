using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace ES.Tools.Core.Infrastructure
{
  public static class ApplicationHelper
  {
    public static string StartupPath => Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

    public static Window ActiveWindow
    {
      get
      {
        Window w = null;
        try
        {
          w = Application.Current?.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
        }
        catch
        {
          return null;
        }
        if (w == null)
        {
          w = MainWindow;
        }

        return w;
      }
    }

    public static Window MainWindow
    {
      get
      {
        try
        {
          return Application.Current?.MainWindow;
        }
        catch
        {
          return null;
        }
      }
    }
  }
}
