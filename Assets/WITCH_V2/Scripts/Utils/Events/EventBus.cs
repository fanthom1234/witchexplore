using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    // cached all subcriber
    // key is Event type(struct)
    // value is list of subscriber of key Event
    private static Dictionary<Type, List<IEventSubcriber>> _eventSubscribers;

    static EventBus()
    {
        _eventSubscribers = new Dictionary<Type, List<IEventSubcriber>>();
    }

    public static void AddSubcriber<Event>(IEventSubcriber newEventSubcriber) where Event : struct
    {
        Type eventType = typeof(Event);

        // create subcriber list if never cached Event type
        if (!_eventSubscribers.ContainsKey(eventType))
        {
            _eventSubscribers.Add(eventType, new List<IEventSubcriber>());
        }

        // add newEventSubcriber to subcriber list of its Event type
        if (!CheckSubcribed(eventType, newEventSubcriber))
        {
            _eventSubscribers[eventType].Add(newEventSubcriber);
        }
    }

    /// <summary>
    /// return true if newSubcriber already subcribed event type
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="newSubcriber"></param>
    /// <returns></returns>
    private static bool CheckSubcribed(Type eventType, IEventSubcriber newSubcriber)
    {
        foreach(IEventSubcriber eventSubcriber in _eventSubscribers[eventType])
        {
            if(eventSubcriber == newSubcriber)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// unsubcribe 
    /// </summary>
    /// <typeparam name="Event"></typeparam>
    /// <param name="removeSubcriber"></param>
    public static void RemoveSubcriber<Event>(IEventSubcriber removeSubcriber) where Event : struct
    {
        Type eventType = typeof(Event);

        // return if there is no subcriber of Event type
        List<IEventSubcriber> currentSubcribers;
        if (!_eventSubscribers.TryGetValue(eventType, out currentSubcribers))
        {
            return;
        }

        if (currentSubcribers.Contains(removeSubcriber))
        {
            currentSubcribers.Remove(removeSubcriber);
        }
    }

    /// <summary>
    /// trigger event for every subcriber
    /// </summary>
    /// <typeparam name="EventBusEvent"></typeparam>
    /// <param name="newEvent"></param>
    public static void TriggerEvent<Event>(Event triggeredEvent) where Event : struct
    {
        List<IEventSubcriber> subcribers = new List<IEventSubcriber>();
        // get list of subcriber of triggeredEvent
        _eventSubscribers.TryGetValue(typeof(Event), out subcribers);

        if (subcribers == null)
        {
            return;
        }

        foreach (IEventSubcriber baseSubcriber in subcribers)
        {
            IEventSubcriber<Event> subcriber = baseSubcriber as IEventSubcriber<Event>;
            if(subcriber != null)
            {
                subcriber.OnEventBusTrigger(triggeredEvent);
            }
        }
    }
}

/// <summary>
/// Static class to start or stop subcribtion
/// </summary>
public static class EventBusRegister
{
    public delegate void Delegate<T>(T eventType);

    public static void EventBusSubcribe<EventType>(this IEventSubcriber<EventType> caller) where EventType : struct
    {
        EventBus.AddSubcriber<EventType>(caller);
    }

    public static void EventBusUnscribe<EventType>(this IEventSubcriber<EventType> caller) where EventType : struct
    {
        EventBus.RemoveSubcriber<EventType>(caller);
    }
}

/// <summary>
/// Event subcriber interface
/// </summary>
public interface IEventSubcriber { };

/// <summary>
/// Interface for each event to subcribe.
/// </summary>
public interface IEventSubcriber<T> : IEventSubcriber
{
    void OnEventBusTrigger(T eventType);
}


