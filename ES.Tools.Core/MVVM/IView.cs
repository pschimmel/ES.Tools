namespace ES.Tools.MVVM
{
  public interface IView
  {
    public object DataContext { get; set; }

    public void Show();

    public bool? ShowDialog();
  }
}
