using System.Windows;

namespace ES.Tools.MVVM
{
  public interface IView
  {
    IViewModel ViewModel { get; set; }
    bool? ShowDialog();
    Window Owner { get; set; }
    bool Topmost { get; set; }
    void Show();
    void Hide();
    void Close();
  }
}
