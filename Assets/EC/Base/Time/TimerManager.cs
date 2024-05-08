using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager:Singleton<TimerManager>
{
    public List<Timer> timers = new List<Timer>();
    public List<Timer> updatetimers = new List<Timer>();

    public override void Init()
    {
        BehaviourCtrl.Instance.OnUpdate += OnUpdate;
    }

    public Timer RegisterTimer(float totalTime, int invokeTime, Action callback,bool firstInvoke = false)
    {
        Timer timer = CachePool.Instance.Get<Timer>();
        timer.Init(totalTime, invokeTime, callback, firstInvoke);   
        timers.Add(timer);
        return timer;
    }

    public void OnUpdate()
    {
        updatetimers.Clear();

        for (int i = 0; i < timers.Count; i++)
        {
            Timer timer = timers[i];
            if(timer.Update(Time.deltaTime))
            {
                timer.Invoke();
            }
            if (timer.invokeTime <= 0)
            {
                updatetimers.Add(timer);
            }
        }

        for (int i = 0; i < updatetimers.Count; i++)
        {
            updatetimers[i].Release();
            timers.Remove(updatetimers[i]);
        }
    }
}
