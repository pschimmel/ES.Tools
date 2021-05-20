using System.Collections.Generic;
using System.Windows.Input;
using ES.Tools.Core.MVVM;

namespace ES.Tools.TestApp.ViewModels
{
  public class MainViewModel : ViewModel
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
      MeterViewModel = new MeterViewModel();
      AccordionItems = new List<AccordionContentViewModel>
      {
        new AccordionContentViewModel("A", "Sample A"),
        new AccordionContentViewModel("B", "Sample B"),
        new AccordionContentViewModel("C", "Sample C"),
        new AccordionContentViewModel("D", "Sample D")
      };
    }

    #endregion

    #region Properties

    public List<ConverterViewModel> Converters { get; }

    public CustomItemsControlViewModel CustomItemsControlViewModel { get; }

    public string SampleWindowText { get; set; }

    public List<AccordionContentViewModel> AccordionItems { get; }

    public MeterViewModel MeterViewModel { get; }

    #endregion

    #region Commands

    public ICommand OpenSampleWindowCommand { get; }

    private void OpenSampleWindowExecute()
    {
      var vm = new SampleWindowViewModel(SampleWindowText);
      var view = ViewFactory.Instance.CreateView(vm);
      view.ShowDialog();
    }

    private bool OpenSampleWindowCanExecute()
    {
      return true;
    }

    #endregion
  }
}
