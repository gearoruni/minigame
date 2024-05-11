using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum State
{
    IDLE,
    MOVE,
    FIRE,
    RELOAD,
    DEATH,
}

public class StateComponent : Component
{
    public bool isVaild = false;

    //public int health = 0;
    public bool isDead = false;
    public State state;

    public override void Init()
    {
        state = State.IDLE;
        isVaild = false;    
    }
    ////public void SetHealth(int health)
    ////{
    ////    this.health = health;
    ////    if (health <= 0)
    ////    {
    ////        isDead = true;
    ////    }
    ////}
    ////public void SetHealthOffset(int offset)
    ////{
    ////    this.health -= offset;
    ////    if (health <= 0)
    ////    {
    ////        isDead = true;
    ////    }
    ////}
    /// <summary>
    /// 默认传入为伤害
    /// 治疗为对应负数
    /// </summary>
    /// <param name="effectData"></param>
    //public void ChangeHealth(int effectData)
    //{
    //    if (isDead)
    //    {
    //        Debug.Log(entity.entityId + " [ 已死亡 ]");
    //        return;
    //    }
    //    SetHealthOffset(effectData);
    //    Debug.Log("造成伤害：" + entity.entityId + " [ " + effectData + " ]");
    //}
    public override void Update()
    {
        if (isDead)
        {
            state = State.DEATH;
            //test
            DestroyComponent destroy = (DestroyComponent)entity.GetComponent("DestroyComponent");
            destroy.Destroy();
        }
    }
    public override void OnCache()
    {
        isVaild = true;
        isDead = false;
        CachePool.Instance.Cache<StateComponent>(this);
    }


}
