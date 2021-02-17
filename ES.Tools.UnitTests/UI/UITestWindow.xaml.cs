using System.Collections.Generic;

namespace ES.Tools.UnitTests.UI
{
  /// <summary>
  /// Interaction logic for UITestWindow.xaml
  /// </summary>
  internal partial class UITestWindow
  {
    public UITestWindow()
    {
      InitializeComponent();
    }

    public List<string> Buttons => new List<string> { "Button1", "Button2", "Button3", "Button4", "Button5", "Button6" };
  }
}
