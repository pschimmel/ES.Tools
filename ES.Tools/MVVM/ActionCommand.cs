using System;

namespace ES.Tools.MVVM
{
  public class ActionCommand : ActionCommand<object>
  {
    public ActionCommand(Action execute, Func<bool> canExecute = null) :
      base(x => execute(), canExecute == null ? (Func<object, bool>)null : x => canExecute())
    { }
  }
}
