using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Xml;

namespace ES.Tools.TestApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      SaveDefaultTemplate();
    }

    public static void SaveDefaultTemplate()
    {
      object control = Application.Current.FindResource(typeof(MenuItem));
      using (var writer = new XmlTextWriter(@"c:\Temp\MenuItem.xml", System.Text.Encoding.UTF8))
      {
        writer.Formatting = Formatting.Indented;
        XamlWriter.Save(control, writer);
      }
    }
  }
}
