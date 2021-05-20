using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ES.Tools.Core.Infrastructure;

namespace ES.Tools.Core.MVVM
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
      _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
      _typeDictionary = new Dictionary<Type, Type>();
    }

    public static ViewFactory Instance => _lazy.Value;

    /// <summary>
    /// Register a new View/ViewModel relationship.
    /// </summary>
    /// <typeparam name="TViewModel">Type of the ViewModel</typeparam>
    /// <typeparam name="TView">Type of the View.</typeparam>
    public void Register<TViewModel, TView>()
        where TViewModel : IViewModel
        where TView : IView
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
    /// Returns an array of the registered ViewModel types.
    /// </summary>
    public Type[] RegisteredViewModelTypes()
    {
      try
      {
        _lock.EnterReadLock();
        return _typeDictionary.Keys.ToArray();
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Returns the View type for a ViewModel type. If the ViewModel type is not registered, but a base class, this will be accepted as well.
    /// </summary>
    /// <returns>Null, if the type was not found.</returns>
    public Type GetViewType(Type viewModelType)
    {
      try
      {
        _lock.EnterReadLock();

        Type viewType = null;

        if (_typeDictionary.ContainsKey(viewModelType))
        {
          viewType = _typeDictionary[viewModelType];
        }
        else
        {
          foreach (var registeredViewModelType in _typeDictionary.Keys)
          {
            if (registeredViewModelType.IsAssignableFrom(viewModelType))
            {
              viewType = _typeDictionary[registeredViewModelType];
              break;
            }
          }
        }

        return viewType;

      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Creates a new matching View with a new instance of the ViewModel.
    /// Optionally takes parameters that are used as parameters for the ViewModels constructor.
    /// </summary>
    public IView CreateView<T>(bool setOwner = true, params object[] args) where T : IViewModel
    {
      var viewModelType = typeof(T);
      var vm = (IViewModel)Activator.CreateInstance(viewModelType, args);
      return CreateView(vm, setOwner);
    }

    /// <summary>
    /// Creates a new matching View for the given ViewModel. If the ViewModel type is not registered, but a base class, this will be accepted as well.
    /// Throws <exception cref="InvalidOperationException"/> when there is no View registered for the ViewModel.
    /// </summary>
    public IView CreateView(IViewModel viewModel, bool setOwner = true)
    {
      _lock.EnterReadLock();
      try
      {
        var viewModelType = viewModel.GetType();
        var viewType = GetViewType(viewModelType);

        if (viewType == null)
        {
          throw new InvalidOperationException("Unknown View for ViewModel object");
        }

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