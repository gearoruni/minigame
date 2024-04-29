using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager :Singleton<EventManager>
{
    private Dictionary<EventName, Event> eventList = new Dictionary<EventName, Event>();

    public void AddListener(EventName eventName, Delegate d)
    {
        Event eventItem;
        if (!eventList.TryGetValue(eventName, out eventItem))
        {
            eventItem = new Event(eventName);
            eventList[eventName] = eventItem;
        }
        eventItem = eventList[eventName];
        eventItem.Add(d);
    }
    
    public void RemoveListener(EventName eventName,Delegate d)
    {
        Event eventItem;
        if (eventList.TryGetValue(eventName, out eventItem))
        {
            if (eventItem == null)
            {
                Debug.LogError(eventName.ToString() + " : 无事件");
                return;
            }
        }
        eventItem.Remove(d);
        if (eventItem.handles.Count <= 0)
        {
            eventList.Remove(eventName);
        }
    }

    public void RemoveAllListener(EventName eventName)
    {
        Event eventItem;
        if (eventList.TryGetValue(eventName, out eventItem))
        {
            if (eventItem == null)
            {
                Debug.LogError(eventName.ToString() + " : 无事件");
                return;
            }
        }
        eventItem.RemoveAll();
    }
    public void Fire(EventName eventName, params object[] parameters)
    {
        Event eventItem;
        if (eventList.TryGetValue(eventName, out eventItem))
        {
            if (eventItem == null)
            {
                Debug.LogError(eventName.ToString() + " : 无事件");
                return;
            }
        }
        eventItem.Fire(parameters);
    }
}
