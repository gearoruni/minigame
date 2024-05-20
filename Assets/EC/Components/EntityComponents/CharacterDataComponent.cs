using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class CharacterDataComponent : Component
{
    public int maxHp;
    public int nowHp;

    public int level = 1;

    public CharacterComponent character;
    public StateComponent state;
    public HitComponent hit;

    public override void Init()
    {
        character = (CharacterComponent)entity.GetComponent("CharacterComponent");

        state = (StateComponent)entity.GetComponent("StateComponent");

        hit = (HitComponent)entity.GetComponent("HitComponent");
    }

    public override void DataInit()
    {
        //血量
        maxHp = character.configs.Hp;
        nowHp = maxHp;

    }
    public override void Update()
    {
        if (hit.healthChangeEffect != null){
            if (hit.healthChangeEffect.CanSetAtk(entity.instanceId))
            {
                ChangeHp(hit.healthChangeEffect.effectData);
            }
            hit.healthChangeEffect = null;
        }
    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<CharacterDataComponent>(this);
    }

    public void SetHealth(int health)
    {
        this.nowHp = health;
        if (health <= 0)
        {
            state.isDead = true;
        }
    }
    public void SetHealthOffset(int offset)
    {
        this.nowHp -= offset;
        if (nowHp <= 0)
        {
            state.isDead = true;
        }
    }
    public void ChangeHp(int effectData)
    {
        if (state.isDead)
        {
            Debug.Log(entity.entityId + " [ 已死亡 ]");
            return;
        }
        SetHealthOffset(effectData);
        Debug.Log("当前生命值：：" + entity.go.name + " [ " + effectData + " ]:[ " + this.nowHp + "]");
    }
}
