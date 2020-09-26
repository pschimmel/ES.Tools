using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ES.Tools.Infrastructure
{
  public static class Services
  {
    private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service) where T : class
    {
      _lock.EnterWriteLock();
      try
      {
        _services[typeof(T)] = service;
      }
      finally
      {
        _lock.ExitWriteLock();
      }
    }

    public static void UnregisterService<T>() where T : class
    {
      if (HasService<T>())
      {
        _lock.EnterWriteLock();
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

    public static T GetService<T>() where T : class
    {
      _lock.EnterReadLock();
      try
      {
        return !HasService<T>()
          ? throw new ApplicationException($"Service of type {typeof(T)} is not registered.")
          : (T)_services[typeof(T)];
      }
      finally
      {
        _lock.ExitReadLock();
      }
    }

    public static bool TryGetService<T>(out T service) where T : class
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

    public static bool HasService<T>() where T : class
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

    public static Type[] ListServices()
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
  }
}
