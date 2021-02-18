using System.Collections.Generic;
using System.Windows.Input;
using ES.Tools.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public class MainViewModel : MVVM.ViewModel
  {
    #region Constructor

    public MainViewModel()
    {
      Converters = new List<ConverterViewModel>
      {
        new ColorToStringConverterViewModel(),
        new TextTrimmingConverterViewModel(),
        new DateTimeToTimeSpanConverterViewModel()
      };

      CustomItemsControlViewModel = new CustomItemsControlViewModel();
      OpenSampleWindowCommand = new ActionCommand(OpenSampleWindowExecute, OpenSampleWindowCanExecute);
    }

    #endregion

    #region Properties

    public List<ConverterViewModel> Converters { get; }

    public CustomItemsControlViewModel CustomItemsControlViewModel { get; }

    public string SampleWindowText { get; set; }

    #endregion

    #region Commands

    public ICommand OpenSampleWindowCommand { get; }

    private void OpenSampleWindowExecute()
    {
      var vm = new SampleWindowViewModel(SampleWindowText);
      var view = ViewFactory.Instance.CreatePage(vm);
      view.ShowDialog();
    }

    private bool OpenSampleWindowCanExecute()
    {
      return true;
    }

    #endregion
  }
}
