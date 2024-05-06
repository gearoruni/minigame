using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public float totalTime;
    public float nowTime;
    public int invokeTime;
    public Action callback;

    public Timer(float totalTime, int invokeTime, Action callback,bool firstInvoke = false)
    {
        this.totalTime = totalTime;
        this.nowTime = firstInvoke?totalTime:0;
        this.invokeTime = invokeTime;
        this.callback = callback;   
    }

    public bool Update(float time)
    {
        this.nowTime += time;
        if(nowTime >= this.totalTime)
        {
            nowTime = 0;
            return true;
        }
        return false;
    }

    public void Invoke()
    {
        if(invokeTime > 0 && callback != null)
        {
            callback();
        }
        invokeTime--;
    }

    public void Release()
    {
        totalTime = 0;
        nowTime=0;
        invokeTime = 0;
        this.callback = null;
    }
}
