﻿using System;
using ES.Tools.MVVM;
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
      ViewFactory.Instance.Register<View1, ViewModel1>();
      ViewFactory.Instance.Register<View2, ViewModel2>();
    }

    [Test]
    public void CreateViewFromTypeTest()
    {
      var view = ViewFactory.Instance.CreatePage<ViewModel1>();
      Assert.That(view, Is.Not.Null);
      Assert.That(view.DataContext, Is.Not.Null);
      Assert.That(view.DataContext, Is.InstanceOf(typeof(ViewModel1)));
    }

    [Test]
    public void CreateViewFromViewModelTest()
    {
      var viewModel = new ViewModel2();
      var view = ViewFactory.Instance.CreatePage(viewModel);
      Assert.That(view, Is.Not.Null);
      Assert.That(view.DataContext, Is.SameAs(viewModel));
    }

    [Test]
    public void CreateViewFromTypeWithParametersTest()
    {
      var view = ViewFactory.Instance.CreatePage<ViewModel2>("Test");
      Assert.That(view, Is.Not.Null);
      Assert.That(view.DataContext, Is.Not.Null);
      Assert.That(view.DataContext, Is.InstanceOf(typeof(ViewModel2)));
      Assert.That((view.DataContext as ViewModel2).Arguments, Is.EqualTo("Test"));
    }

    [Test]
    public void RegisterSameViewModelTwiceTest()
    {
      Assert.Throws<InvalidOperationException>(() => ViewFactory.Instance.Register<View1, ViewModel1>());
    }

    [Test]
    public void RegisterSameViewTwiceTest()
    {
      Assert.Throws<InvalidOperationException>(() => ViewFactory.Instance.Register<View1, ViewModel3>());
    }

    [Test]
    public void CreateUnknownViewFromTypeTest()
    {
      Assert.Throws<InvalidOperationException>(() => ViewFactory.Instance.CreatePage<ViewModel4>());
    }

    internal class View1 : IView
    {
      public object DataContext { get; set; }

      public void Show() { }

      public bool? ShowDialog()
      {
        return true;
      }
    }

    internal class View2 : IView
    {
      public object DataContext { get; set; }

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
  }
}
