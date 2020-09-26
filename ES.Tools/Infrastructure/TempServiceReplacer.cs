using System;

namespace ES.Tools.Infrastructure
{
  public class TempServiceReplacer<T> : IDisposable where T : class
  {
    private readonly T _serviceBackup = null;
    private bool _isDisposed = false;

    public TempServiceReplacer(T service)
    {
      _serviceBackup = Services.HasService<T>() ? Services.GetService<T>() : null;
      Services.RegisterService(service);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="ServiceReplacer"/>.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="ServiceReplacer"/>.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
      if (_isDisposed)
      {
        return;
      }

      if (disposing)
      {
        // free managed resources

        if (Services.HasService<T>())
        {
          var replacement = Services.GetService<T>();
          if (replacement is IDisposable disposable)
          {
            Services.UnregisterService<T>();
            disposable.Dispose();
          }
        }

        Services.RegisterService(_serviceBackup);
      }

      // free native resources if there are any.

      _isDisposed = true;
    }
  }
}
