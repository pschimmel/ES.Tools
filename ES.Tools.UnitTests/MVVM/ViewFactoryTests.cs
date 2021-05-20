using System;
using System.Linq;
using System.Windows;
using ES.Tools.Core.MVVM;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ES.Tools.UnitTests.MVVM
{
  /// <summary>
  /// Unit tests testing the <see cref="ViewFactory"/>
  /// </summary>
  public class ViewFactoryTests
  {
    [OneTimeSetUp]
    public void Initialize()
    {
      ViewFactory.Instance.Register<ViewModel1, View1>();
      ViewFactory.Instance.Register<ViewModel2, View2>();
    }

    [Test]
    public void GetViewModelTypesTest()
    {
      var types = ViewFactory.Instance.RegisteredViewModelTypes();

      Assert.That(types.Count(), Is.EqualTo(2));
      Assert.That(types, Contains.Item(typeof(ViewModel1)));
      Assert.That(types, Contains.Item(typeof(ViewModel2)));
    }

    [Test]
    public void GetViewTypeTest()
    {
      Assert.That(ViewFactory.Instance.GetViewType(typeof(ViewModel1)), Is.EqualTo(typeof(View1)));
      Assert.That(ViewFactory.Instance.GetViewType(typeof(ViewModel2)), Is.EqualTo(typeof(View2)));
      Assert.That(ViewFactory.Instance.GetViewType(typeof(ViewModel3)), Is.Null);
      Assert.That(ViewFactory.Instance.GetViewType(typeof(GenericViewModel<int>)), Is.EqualTo(typeof(View1)));
    }

    [Test]
    public void CreateViewFromTypeTest()
    {
      var view = ViewFactory.Instance.CreateView<ViewModel1>();
      Assert.That(view, Is.Not.Null);
      Assert.That(view.ViewModel, Is.Not.Null);
      Assert.That(view.ViewModel, Is.InstanceOf(typeof(ViewModel1)));
    }

    [Test]
    public void CreateViewFromViewModelTest()
    {
      var viewModel = new ViewModel2();
      var view = ViewFactory.Instance.CreateView(viewModel);
      Assert.That(view, Is.Not.Null);
      Assert.That(view.ViewModel, Is.SameAs(viewModel));
    }

    [Test]
    public void CreateViewFromTypeWithParametersTest()
    {
      var view = ViewFactory.Instance.CreateView<ViewModel2>(false, "Test");
      Assert.That(view, Is.Not.Null);
      Assert.That(view.ViewModel, Is.Not.Null);
      Assert.That(view.ViewModel, Is.InstanceOf(typeof(ViewModel2)));
      Assert.That((view.ViewModel as ViewModel2).Arguments, Is.EqualTo("Test"));
    }

    [Test]
    public void CreateViewFromGenericDerivedTypTest()
    {
      var viewModel = new GenericViewModel<string>();
      var view = ViewFactory.Instance.CreateView(viewModel);
      Assert.That(view, Is.Not.Null);
      Assert.That(view.ViewModel, Is.Not.Null);
      Assert.That(view.ViewModel, Is.InstanceOf(typeof(GenericViewModel<string>)));
      Assert.That(view.ViewModel, Is.AssignableTo(typeof(ViewModel1)));
    }

    [Test]
    public void RegisterSameViewModelTwiceTest()
    {
      Assert.Throws<InvalidOperationException>(() => ViewFactory.Instance.Register<ViewModel1, View1>());
    }

    [Test]
    public void RegisterSameViewTwiceTest()
    {
      Assert.Throws<InvalidOperationException>(() => ViewFactory.Instance.Register<ViewModel3, View1>());
    }

    [Test]
    public void CreateUnknownViewFromTypeTest()
    {
      Assert.Throws<InvalidOperationException>(() => ViewFactory.Instance.CreateView<ViewModel4>());
    }

    internal class View1 : IView
    {
      public IViewModel ViewModel { get; set; }

      public Window Owner { get; set; }

      public bool Topmost { get; set; }

      public void Close() { }

      public void Hide() { }

      public void Show() { }

      public bool? ShowDialog()
      {
        return true;
      }
    }

    internal class View2 : IView
    {
      public IViewModel ViewModel { get; set; }

      public Window Owner { get; set; }

      public bool Topmost { get; set; }

      public void Close() { }

      public void Hide() { }

      public void Show() { }

      public bool? ShowDialog()
      {
        return true;
      }
    }

    internal class ViewModel1 : ViewModel
    {
    }

    internal class ViewModel2 : ViewModel
    {
      public ViewModel2()
      { }

      public ViewModel2(string args)
      {
        Arguments = args;
      }

      public string Arguments { get; }
    }

    internal class ViewModel3 : ViewModel
    {
    }

    internal class ViewModel4 : ViewModel
    {
    }

    internal class GenericViewModel<T> : ViewModel1
    {
    }
  }
}
