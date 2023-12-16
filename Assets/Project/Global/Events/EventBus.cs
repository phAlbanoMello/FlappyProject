using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, List<Action<object>>> _eventHandlers;

    static EventBus()
    {
        _eventHandlers = new Dictionary<Type, List<Action<object>>>();
    }

    public static void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (!_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = new List<Action<object>>();
        }

        Action<object> wrappedHandler = (param) => handler((T)param);
        _eventHandlers[eventType].Add(wrappedHandler);
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            Action<object> wrappedHandler = (param) => handler((T)param);
            _eventHandlers[eventType].Remove(wrappedHandler);
        }
    }

    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            foreach (var handler in _eventHandlers[eventType])
            {
                handler.Invoke(eventData);
            }
        }
    }
}
