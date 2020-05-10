using System;
using System.Windows;
using System.Windows.Threading;

namespace ES.Tools.Base
{
  /// <summary>
  /// Wraps a dispatcher to execute actions on the UI thread.
  /// </summary>
  public class DispatcherWrapper : IDispatcherWrapper
  {
    private readonly Dispatcher _dispatcher;

    public DispatcherWrapper(Dispatcher dispatcher)
    {
      _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    /// <summary>
    /// Executes an action asynchronuously on the UI thread if necessary.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    /// <param name="priority">Dispatcher priority.</param>
    public void BeginInvokeIfRequired(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      if (!_dispatcher.CheckAccess())
      {
        _dispatcher.BeginInvoke(action, priority);
      }
      else
      {
        action.Invoke();
      }
    }

    /// <summary>
    /// Executes an action synchronuously on the UI thread if necessary.
    /// </summary>
    /// <param name="action">Action to be executed.</param>
    /// <param name="priority">Dispatcher priority.</param>
    public void InvokeIfRequired(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      // Check if this thread has access to the object.
      if (_dispatcher.CheckAccess())
      {
        // This thread has access so it can update the UI thread.
        action.Invoke();
      }
      else
      {
        // This thread does not have access to the UI thread.
        // Run the update method on the Dispatcher of the UI thread.
        _dispatcher.Invoke(priority, action);
      }
    }

    /// <summary>
    /// Returns a <see cref="DispatcherWrapper"/> containing the default application dispatcher.
    /// </summary>
    public static IDispatcherWrapper Default => new DispatcherWrapper(Application.Current.Dispatcher);
  }
}
