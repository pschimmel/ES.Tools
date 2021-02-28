using System;

namespace ES.Tools.Infrastructure
{
  /// <summary>
  /// Temporarily replaces a service registered in the <see cref="Services"/> registry.
  /// </summary>
  public class TempServiceReplacer<T> : IDisposable where T : class
  {
    private readonly T _serviceBackup = null;
    private bool _isDisposed = false;

    /// <summary>
    /// Constructor
    /// </summary>
    public TempServiceReplacer(T service)
    {
      _serviceBackup = Services.Instance.HasService<T>() ? Services.Instance.GetService<T>() : null;
      Services.Instance.UnregisterService<T>();
      Services.Instance.RegisterService(service);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="TempServiceReplacer{T}"/>.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="TempServiceReplacer{T}"/>.
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

        if (Services.Instance.HasService<T>())
        {
          var replacement = Services.Instance.GetService<T>();
          Services.Instance.UnregisterService<T>();

          if (replacement is IDisposable disposable)
          {
            disposable.Dispose();
          }
        }

        Services.Instance.RegisterService(_serviceBackup);
      }

      // free native resources if there are any.

      _isDisposed = true;
    }
  }
}
