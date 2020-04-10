using System;

namespace ES.MVVM
{
  public abstract class ViewModel : NotifyObject, IDisposable
  {
    protected ViewModel()
    {
    }

    // Dispose() calls Dispose(true)
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    // The bulk of the clean-up code is implemented in Dispose(bool)
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        // free managed resources
      }
      // free native resources if there are any.
    }
  }
}
