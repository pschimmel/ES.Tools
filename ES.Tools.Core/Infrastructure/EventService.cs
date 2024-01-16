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

    /// <summary>
    /// Subscribe for an event.
    /// </summary>
    public void Subscribe<T>(Type type, Action<T> action)
    {
      Subscribe(type?.FullName, action);
    }

    /// <summary>
    /// Subscribe for an event.
    /// </summary>
    public void Subscribe<T>(string name, Action<T> action)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentException("Name cannot be null or emtpy.");
      }

      if (action == null)
      {
        throw new ArgumentNullException(nameof(action));
      }

      if (!_events.TryGetValue(name, out var value))
      {
        value = new List<Action<T>>();
        _events[name] = value;
      }

      value.Add(action);
    }

    /// <summary>
    /// Unubscribe an event.
    /// </summary>
    public void Unsubscribe(string name)
    {
      _events.Remove(name);
    }

    /// <summary>
    /// Publish an event. All subscribers will be notified.
    /// </summary>
    public void Publish<T>(Type type, T payload)
    {
      Publish(type?.FullName, payload);
    }

    /// <summary>
    /// Publish an event. All subscribers will be notified.
    /// </summary>
    public void Publish<T>(string name, T payload)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentException("Name cannot be null or emtpy.");
      }

      if (_events.TryGetValue(name, out IList delegateList))
      {
        if (delegateList != null)
        {
          foreach (var delegateItem in delegateList)
          {
            ((Action<T>)delegateItem).Invoke(payload);
          }
        }
      }
    }
  }
}