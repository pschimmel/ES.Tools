using ES.Tools.MVVM;
using NUnit.Framework;

namespace ES.Tools.UnitTests.MVVM
{
  /// <summary>
  /// Unit tests testing the <see cref="ActionCommand"/>
  /// </summary>
  public class ActionCommandTests
  {
    [Test]
    public void ActionCommandExecuteTest()
    {
      bool executed = false;
      var command = new ActionCommand(() => executed = true);

      // Not executed yet.
      Assert.IsFalse(executed);

      // No delegate passed => CanExecute must be true.
      Assert.IsTrue(command.CanExecute(null));

      command.Execute(null);
      // Executed.
      Assert.IsTrue(executed);
    }

    [Test]
    public void ActionCommandCanExecuteTest()
    {
      bool canExecute = false;
      bool canExecuteChanged = false;
      var command = new ActionCommand(() => { }, () => canExecute);
      command.CanExecuteChanged += (s, e) =>
      {
        canExecuteChanged = true;
      };

      // Not executed yet.
      Assert.IsFalse(canExecute);
      Assert.IsFalse(canExecuteChanged);

      // Cannot execute.
      Assert.IsFalse(command.CanExecute(null));
      Assert.IsFalse(canExecuteChanged);

      canExecute = true;
      // Now we can execute, but no event was triggered.
      Assert.IsTrue(command.CanExecute(null));
      Assert.IsFalse(canExecuteChanged);

      command.RaiseCanExecuteChanged();
      // Now we can execute, event was triggered.
      Assert.IsTrue(command.CanExecute(null));
      Assert.IsTrue(canExecuteChanged);
    }

    [Test]
    public void ActionCommandOfTExecuteTest()
    {
      bool executed = false;
      int parameter = 0;

      var command = new ActionCommand<int>((d) =>
      {
        executed = true;
        parameter = d;
      });

      // Not executed yet.
      Assert.IsFalse(executed);

      // No delegate passed => CanExecute must be true.
      Assert.IsTrue(command.CanExecute(12));

      command.Execute(12);
      // Executed.
      Assert.IsTrue(executed);
      Assert.AreEqual(12, parameter);
    }

    [Test]
    public void ActionCommandOfTCanExecuteTest()
    {
      bool canExecuteChanged = false;
      var command = new ActionCommand<int>((d) => { }, (d) =>
      {
        return d == 12;
      });
      command.CanExecuteChanged += (s, e) =>
      {
        canExecuteChanged = true;
      };

      // Not executed yet.
      Assert.IsFalse(canExecuteChanged);

      // Cannot execute.
      Assert.IsFalse(command.CanExecute(5));
      Assert.IsFalse(canExecuteChanged);

      // Now we can execute, but no event was triggered.
      Assert.IsTrue(command.CanExecute(12));
      Assert.IsFalse(canExecuteChanged);

      command.RaiseCanExecuteChanged();
      // Now we can execute, event was triggered.
      Assert.IsTrue(command.CanExecute(12));
      Assert.IsTrue(canExecuteChanged);
    }
  }
}