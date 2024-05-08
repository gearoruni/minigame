using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCmp : MonoBehaviour
{
    public bool cache = false;

    public int id;
    public float livingTime;

    private Timer timer;
    private string tag;
    public void Init(bool cache, int id, float livingTime)
    {
        this.cache = cache;
        this.id = id;
        this.livingTime = livingTime;
        tag = this.gameObject.tag;
    }

    public void CreateDelayDestroyTimer()
    {
        timer = null;
        //timer = GameCore.Instance.GetModel<TimerCtrl>().RegisterTimer(livingTime, 1, DestroySelf);
    }

    public void DestroySelf()
    {
        if(this.cache)
        {
            ObjectPoolManager.Instance.ReturnObjectToPool(id, this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(timer!=null) timer.callback -= DestroySelf;
    }
}
