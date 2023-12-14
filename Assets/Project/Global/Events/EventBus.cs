using System;
using System.Collections.Generic;

public static class EventBus
{
    private static Dictionary<Type, List<Action<object>>> eventHandlers;

    static EventBus()
    {
        eventHandlers = new Dictionary<Type, List<Action<object>>>();
    }

    public static void Subscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (!eventHandlers.ContainsKey(eventType))
        {
            eventHandlers[eventType] = new List<Action<object>>();
        }

        Action<object> wrappedHandler = (param) => handler((T)param);
        eventHandlers[eventType].Add(wrappedHandler);
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        Type eventType = typeof(T);

        if (eventHandlers.ContainsKey(eventType))
        {
            Action<object> wrappedHandler = (param) => handler((T)param);
            eventHandlers[eventType].Remove(wrappedHandler);
        }
    }

    public static void Publish<T>(T eventData)
    {
        Type eventType = typeof(T);

        if (eventHandlers.ContainsKey(eventType))
        {
            foreach (var handler in eventHandlers[eventType])
            {
                handler.Invoke(eventData);
            }
        }
    }
}
