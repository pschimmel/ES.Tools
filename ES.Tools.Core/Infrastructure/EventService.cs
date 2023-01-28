using System;
using System.Collections;
using System.Collections.Generic;

namespace ES.Tools.Core.Infrastructure
{
  public class EventService
  {
    private static readonly Lazy<EventService> _service = new(()=> new EventService());
    private readonly Dictionary<string, IList> _events;

    private EventService()
    {
      _events = new Dictionary<string, IList>();
    }

    public static EventService Instance => _service.Value;

    public void Subscribe<T>(Type type, Action<T> action)
    {
      Subscribe(type?.FullName ?? throw new ArgumentNullException(nameof(type), "Type name cannot be empty."), action);
    }

    public void Subscribe<T>(string name, Action<T> action)
    {
      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      if (!_events.ContainsKey(name))
      {
        _events[name] = new List<Action<T>>();
      }

      _events[name].Add(action);
    }

    public void Unsubscribe(string name)
    {
      _events.Remove(name);
    }

    public void Publish<T>(Type type, T payload)
    {
      Publish(type?.FullName ?? throw new ArgumentNullException(nameof(type), "Type name cannot be empty."), payload);
    }

    public void Publish<T>(string name, T payload)
    {
      if (_events.TryGetValue(name, out IList delegateList))
      {
        if (delegateList == null)
        {
          throw new ArgumentOutOfRangeException($"The named event '{name}' was not registered.");
        }

        foreach (var delegateItem in delegateList)
        {
          ((Action<T>)delegateItem).Invoke(payload);
        }
      }
      else
      {
        throw new ArgumentOutOfRangeException($"The named event '{name}' was not registered.");
      }
    }
  }
}
