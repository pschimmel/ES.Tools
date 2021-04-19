using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ES.Tools.Core.Infrastructure;

namespace ES.Tools.MVVM
{
  /// <summary>
  /// Factory that manages the relationship between Views and ViewModels.
  /// </summary>
  public class ViewFactory
  {
    private static readonly Lazy<ViewFactory> _lazy = new(() => new ViewFactory());
    private readonly ReaderWriterLockSlim _lock;
    private readonly Dictionary<Type, Type> _typeDictionary;

    private ViewFactory()
    {
      _lock = new ReaderWriterLockSlim();
      _typeDictionary = new Dictionary<Type, Type>();
    }

    public static ViewFactory Instance => _lazy.Value;

    /// <summary>
    /// Register a new View/ViewModel relationship.
    /// </summary>
    /// <typeparam name="TView">Type of the View.</typeparam>
    /// <typeparam name="TViewModel">Type of the ViewModel</typeparam>
    public void Register<TView, TViewModel>()
        where TView : IView
        where TViewModel : IViewModel
    {
      _lock.EnterWriteLock();
      try
      {
        if (_typeDictionary.ContainsKey(typeof(TViewModel)))
        {
          throw new InvalidOperationException($"A ViewModel of type {typeof(TViewModel).Name} is already registered.");
        }

        if (_typeDictionary.Values.Contains(typeof(TView)))
        {
          throw new InvalidOperationException($"A View of type {typeof(TView).Name} is already registered.");
        }

        _typeDictionary[typeof(TViewModel)] = typeof(TView);
      }
      finally
      {
        _lock.ExitWriteLock();
      }
    }

    /// <summary>
    /// Creates a new matching View with a new instance of the the ViewModel.
    /// Optionally takes parameters that are used as parameters for the ViewModels constructor.
    /// </summary>
    public IView CreateView<T>(params object[] args) where T : IViewModel
    {
      var viewModelType = typeof(T);
      var vm = (IViewModel)Activator.CreateInstance(viewModelType, args);
      return CreateView(vm);
    }

    /// <summary>
    /// Creates a new matching View for the given ViewModel.
    /// Throws <exception cref="InvalidOperationException"/> when there is no View registered for the ViewModel.
    /// </summary>
    public IView CreateView(IViewModel viewModel, bool setOwner = true)
    {
      _lock.EnterReadLock();
      try
      {
        var viewType = _typeDictionary.ContainsKey(viewModel.GetType())
          ? _typeDictionary[viewModel.GetType()]
          : throw new InvalidOperationException("Unknown View for ViewModel object");

        var view = (IView)Activator.CreateInstance(viewType);
        view.ViewModel = viewModel;

        if (setOwner && !view.Topmost)
        {
          view.Owner = ApplicationHelper.ActiveWindow;
        }

        return view;
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }
  }
}