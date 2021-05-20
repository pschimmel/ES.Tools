using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ES.Tools.Core.MVVM
{
  /// <summary>
  /// Implementation of a generic action command.
  /// </summary>
  public class ActionCommand<T> : ICommand
  {
    private event EventHandler InternalCanExecuteChanged;

    private readonly Action<T> execute;
    private readonly Func<T, bool> canExecute;

    public ActionCommand(Action<T> execute, Func<T, bool> canExecute = null)
    {
      this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
      this.canExecute = canExecute;
    }

    public event EventHandler CanExecuteChanged
    {
      add
      {
        InternalCanExecuteChanged += value;
        CommandManager.RequerySuggested += value;
      }
      remove
      {
        InternalCanExecuteChanged -= value;
        CommandManager.RequerySuggested -= value;
      }
    }

    public void Execute(object parameter)
    {
      execute((T)parameter);
    }

    [DebuggerStepThrough]
    public bool CanExecute(object parameter)
    {
      return canExecute == null || canExecute((T)parameter);
    }

    /// <summary>
    /// This method can be used to raise the CanExecuteChanged handler.
    /// This will force WPF to re-query the status of this command directly.
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
      if (canExecute != null)
      {
        OnCanExecuteChanged();
      }
    }

    /// <summary>
    /// This method is used to walk the delegate chain and well WPF that
    /// our command execution status has changed.
    /// </summary>
    protected virtual void OnCanExecuteChanged()
    {
      InternalCanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
  }
}
