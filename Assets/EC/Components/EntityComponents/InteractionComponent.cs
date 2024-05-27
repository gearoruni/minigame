using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionComponent : Component
{
    CollisionComponent collision;

    bool canInteraction = false;

    public override void Init()
    {
        collision = (CollisionComponent)entity.GetComponent("CollisionComponent");
    }

    public override void DataInit()
    {
        collision.OnBaseTriggerEnter2D += Invoke;
        collision.OnBaseTriggerExit2D += DisAble;

    }

    public void Invoke(Entity entity)
    {
        canInteraction = true;
        Debug.Log("�ȴ����� �� F �� ���н���");

    }

    public override void Update()
    {
        if(canInteraction && Input.GetKeyDown(KeyCode.F))
        {

        }
    }

    public void DisAble(Entity entity)
    {
        canInteraction = false;
        Debug.Log("�뿪��������");

    }
    public override void OnCache()
    {
        CachePool.Instance.Cache<HitComponent>(this);
    }
}