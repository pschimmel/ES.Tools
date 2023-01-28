namespace ES.Tools.Core.MVVM
{
  public interface IView<T> : IView where T : class, IViewModel
  {
    new T ViewModel { get; set; }
  }
}
