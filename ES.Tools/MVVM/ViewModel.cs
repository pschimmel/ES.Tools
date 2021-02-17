using System;

namespace ES.Tools.MVVM
{
  /// <summary>
  /// Base class for View Models. Implements IDisposable.
  /// </summary>
  public abstract class ViewModel : NotifyObject, IViewModel
  {
    private bool _disposed = false; // To detect redundant calls

    /// <summary>
    /// Implementation of IDisposable. Calls Dispose(true).
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // The bulk of the clean-up code is implemented in Dispose(bool)
    protected virtual void Dispose(bool disposing)
    {
      if (_disposed)
      {
        return;
      }

      if (disposing)
      {
        // free managed resources
      }

      // free native resources if there are any.
      _disposed = true;
    }
  }
}
