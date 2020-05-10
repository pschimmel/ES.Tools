using System;
using System.Windows.Threading;

namespace ES.Tools
{
  /// <summary>
  /// Wraps a dispatcher to execute actions on the UI thread.
  /// Replace the dispatcher with a dummy implementation during unit testing.
  /// </summary>
  public interface IDispatcherWrapper
  {
    void BeginInvokeIfRequired(Action callback, DispatcherPriority priority = DispatcherPriority.Normal);

    void InvokeIfRequired(Action callback, DispatcherPriority priority = DispatcherPriority.Normal);
  }
}
