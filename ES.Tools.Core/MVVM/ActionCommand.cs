using System;

namespace ES.Tools.MVVM
{
  /// <summary>
  /// Action command without a specific object as parameter.
  /// </summary>
  public class ActionCommand : ActionCommand<object>
  {
    public ActionCommand(Action execute, Func<bool> canExecute = null)
      : base(x => execute(), canExecute == null ? null : x => canExecute())
    { }
  }
}
