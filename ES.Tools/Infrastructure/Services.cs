using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ES.Tools.Infrastructure
{
  /// <summary>
  /// Provides an infrastructure for services. Can be used as simple DI container.
  /// </summary>
  public class Services
  {
    private static readonly Lazy<Services> _lazy = new Lazy<Services>(()=> new Services());
    private readonly ReaderWriterLockSlim _lock;
    private readonly Dictionary<Type, object> _services;

    private Services()
    {
      _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
      _services = new Dictionary<Type, object>();
    }

    public static Services Instance => _lazy.Value;

    /// <summary>
    /// Registers a service for a certain type.
    /// </summary>
    public void RegisterService<T>(T service) where T : class
    {
      _lock.EnterWriteLock();
      try
      {

        if (_services.ContainsKey(typeof(T)))
        {
          throw new InvalidOperationException("Service is already registered.");
        }

        _services[typeof(T)] = service;
      }
      finally
      {
        _lock.ExitWriteLock();
      }
    }

    /// <summary>
    /// Unregisters a service for a certain type.
    /// </summary>
    public void UnregisterService<T>() where T : class
    {
      _lock.EnterWriteLock();
      if (HasService<T>())
      {
        try
        {
          _services.Remove(typeof(T));
        }
        finally
        {
          _lock.ExitWriteLock();
        }
      }
    }

    /// <summary>
    /// Returns a service for a certain type.
    /// Throws <exception cref="InvalidOperationException"/> when the requested type is not registered.
    /// </summary>
    public T GetService<T>() where T : class
    {
      _lock.EnterReadLock();
      try
      {
        return !HasService<T>()
          ? throw new InvalidOperationException($"Service of type {typeof(T)} is not registered.")
          : (T)_services[typeof(T)];
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Tries to get a service for a certain type.
    /// Returns true if the type could be retrieved, otherwise false.
    /// </summary>
    public bool TryGetService<T>(out T service) where T : class
    {
      _lock.EnterReadLock();
      try
      {
        service = default;

        if (_services.TryGetValue(typeof(T), out object value) && value is T valueAsT)
        {
          service = valueAsT;
          return true;
        }

        return false;
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Checks if a service for a certain type is registered.
    /// </summary>
    public bool HasService<T>() where T : class
    {
      _lock.EnterReadLock();
      try
      {
        return _services.ContainsKey(typeof(T));
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Returns a list of all registered service types.
    /// </summary>
    public Type[] ListServices()
    {
      _lock.EnterReadLock();
      try
      {
        return _services.Keys.ToArray();
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }


    /// <summary>
    /// Removes all services.
    /// </summary>
    public void Clear()
    {
      _lock.EnterWriteLock();
      try
      {
        _services.Clear();
      }
      finally
      {
        _lock.ExitWriteLock();
      }
    }
  }
}
