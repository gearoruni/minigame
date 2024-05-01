using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCmp : MonoBehaviour
{
    public BulletData data;
    public BulletAction bulletAction;
    public HurtCmp hurtCmp;
    public void Init(BulletData data)
    {
        this.data = data;

        bulletAction = this.gameObject.AddComponent<BulletAction>();

        hurtCmp = this.gameObject.AddComponent<HurtCmp>();
    }

    public void SetBulletAction()
    {
        bulletAction.Init(data, this.gameObject);
    }

    public void SetHurtCmp(string ComponentId)
    {
        hurtCmp.Init(ComponentId, data.demage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
