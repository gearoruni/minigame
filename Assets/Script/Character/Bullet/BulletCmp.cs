using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BulletCmp: MonoBehaviour
{
    public BulletData data;

    public DestroyCmp destroyCmp;
    public HurtCmp hurtCmp;
    public bool hasInit = false;
    public void Init(BulletData data)
    {
        this.data = data;
        if (hasInit ==false ) {
            
            destroyCmp = this.gameObject.AddComponent<DestroyCmp>();
            hurtCmp = this.gameObject.AddComponent<HurtCmp>();
        }
        hasInit = true;
    }

    public void SetDestroyCmp(bool cache)
    {
        destroyCmp.Init(cache,data.id,data.livingTime);
        destroyCmp.CreateDelayDestroyTimer();
    }

    public void SetHurtCmp(string ComponentId)
    {
        hurtCmp.Init(ComponentId, data.demage);
        //hurtCmp.AddCallback(destroyCmp.DestroySelf);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != destroyCmp.tag && collision.tag!="Grid")
        {
            destroyCmp.DestroySelf();
        }
    }
}
