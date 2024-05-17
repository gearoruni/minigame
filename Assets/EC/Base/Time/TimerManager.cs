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
    /// <summary>
    /// 注册计时器
    /// 切记：只有不需要处理计时后续操作的action可以使用这个Timer
    /// </summary>
    /// <param name="totalTime">间隔时间</param>
    /// <param name="invokeTime">调用次数</param>
    /// <param name="callback">回调</param>
    /// <param name="firstInvoke">最开始的时候是否触发</param>
    /// <returns></returns>
    public Timer RegisterTimer(float totalTime, int invokeTime, Action callback,bool firstInvoke = false)
    {
        Timer timer = CachePool.Instance.Get<Timer>();
        timer.Init(totalTime, invokeTime, callback, firstInvoke);   
        timers.Add(timer);
        return timer;
    }
    public void RemoveTimer(Timer timer)
    {
        if(timer.isVaild)
        {
            updatetimers.Add(timer);
        }
    }
    public void OnUpdate()
    {
        for (int i = 0; i < updatetimers.Count; i++)
        {
            updatetimers[i].Release();
            timers.Remove(updatetimers[i]);
        }

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

    }
}
