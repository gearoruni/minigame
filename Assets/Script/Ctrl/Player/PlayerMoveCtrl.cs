using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl: BaseModel
{
    public GameObject player;
    public Rigidbody2D rb;

    public CharacterData data;
    //速度应该读表从data取，现在没写
    public float speed = 100f;

    public void PlayerMove(Vector2 direction)
    {
        MoveAction.MoveGo(player, speed, direction);
    }

    #region 初始化
    public bool Init()
    {
        player = GameObject.Find("Player");

        rb = player.GetComponentInChildren<Rigidbody2D>();
        data = player.GetComponent<CharacterData>();
        return true;
    }
    public UniTask<bool> InitAysnc()
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region 生命周期
    public void OnFixUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnStart()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }
    #endregion

}
