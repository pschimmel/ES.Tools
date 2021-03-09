using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ES.Tools.MVVM;
using ES.Tools.UI;

namespace ES.Tools.TestApp.ViewModels
{
  public class MeterViewModel : ViewModel
  {
    private double _value = 20;
    private bool _isGenerating = false;
    private int mainTicks;
    private int subTicks;

    public MeterViewModel()
    {
      RandomValuesCommand = new ActionCommand(RandomValuesCommandExecute);
    }

    public double Value
    {
      get => _value;
      set
      {
        if (_value != value)
        {
          _value = value;
          OnPropertyChanged(nameof(Value));
        }
      }
    }


    public int MainTicks
    {
      get => mainTicks;
      set { mainTicks = value; OnPropertyChanged(); }
    }

    public int SubTicks
    {
      get => subTicks;
      set { subTicks = value; OnPropertyChanged(); }
    }


    public double MinValue => 0;

    public double MaxValue => 100;

    public ICommand RandomValuesCommand { get; }

    private void RandomValuesCommandExecute()
    {
      var random = new Random(DateTime.Now.Millisecond);
      (RandomValuesCommand as ActionCommand).RaiseCanExecuteChanged();

      _isGenerating = !_isGenerating;

      if (_isGenerating)
      {
        Task.Run(() =>
        {
          while (_isGenerating)
          {
            double next = (random.NextDouble() + double.Epsilon) * 10 - 5; // Random number between -5 and 5
            double nextValue = Math.Max(MinValue, Value + next);
            nextValue = Math.Min(MaxValue, nextValue);
            Value = nextValue;
            Thread.Sleep(100);
          }
          DispatcherWrapper.Default.InvokeIfRequired(() => (RandomValuesCommand as ActionCommand).RaiseCanExecuteChanged());
        });
      }
    }
  }
}
