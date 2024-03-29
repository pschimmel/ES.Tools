﻿using System.Windows;
using ES.Tools.Core.MVVM;
using ES.Tools.TestApp.ViewModels;
using ES.Tools.TestApp.Views;

namespace ES.Tools.TestApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public App()
    {
      ViewFactory.Instance.Register<SampleWindowViewModel, SampleWindow>();
    }
  }
}
