using System;

namespace ES.Tools.Core.MVVM
{
  /// <summary>
  /// Action command without a specific object as parameter.
  /// </summary>
  public class ActionCommand : ActionCommand<object>
  {
    public ActionCommand(Action execute, Func<bool> canExecute = null)
      : this(x => execute(), canExecute == null ? null : x => canExecute())
    { }

    public ActionCommand(Action<object> execute, Predicate<object> canExecute = null)
      : base(execute, canExecute)
    { }
  }
}
