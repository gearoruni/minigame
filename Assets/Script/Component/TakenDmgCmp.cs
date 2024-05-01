using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakenDmgCmp : MonoBehaviour
{
    //交给父类信息管理 hp，创建GO时传入
    [SerializeField]
    private int hp = 100;

    public string ComponentId;

    public string cmpTag;
    public void Init(string ComponentId,int Hp)
    {
        this.ComponentId = ComponentId;
        this.hp = Hp;
        this.cmpTag = gameObject.tag;
    }

    public int GetHp()
    {
        return hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HurtCmp atker = collision.gameObject.GetComponent<HurtCmp>();
        int dmg = atker.dmg;
        string atkerTag = atker.tag;
        if (atker != null && atkerTag != cmpTag)
        {
            hp = HurtAction.TakenDmg(dmg, hp);
            Debug.Log(string.Format("造成 {0} 伤害，剩余血量 ：{1}", dmg, hp));
        }
    }

}
