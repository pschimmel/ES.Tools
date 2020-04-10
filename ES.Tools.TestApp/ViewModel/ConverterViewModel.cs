﻿namespace ES.Tools.TestApp.ViewModel
{
  public abstract class ConverterViewModel : MVVM.ViewModel
  {
    protected ConverterViewModel(string description)
    {
      Description = description;
    }

    public string Description { get; }
  }
}
