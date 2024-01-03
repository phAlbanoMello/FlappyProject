using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, List<Delegate>> _eventHandlers;

    static EventBus()
    {
        _eventHandlers = new Dictionary<Type, List<Delegate>>();
    }

    public static void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (!_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType] = new List<Delegate>();
        }

        _eventHandlers[eventType].Add(handler);
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            _eventHandlers[eventType].Remove(handler);
        }
    }

    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);

        if (_eventHandlers.ContainsKey(eventType))
        {
            List<Delegate> currentHandlers = new List<Delegate>(_eventHandlers[eventType]);

            foreach (var handler in currentHandlers)
            {
                if (handler is Action<T>)
                {
                    ((Action<T>)handler).Invoke(eventData);
                }
            }
        }
    }
}
