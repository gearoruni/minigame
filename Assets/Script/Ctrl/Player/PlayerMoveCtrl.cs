using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl: BaseModel
{
    public GameObject player;

    public CharacterData data;
    //速度应该读表从data取，现在没写
    public float speed = 5f;

    public void PlayerMove(Vector2 direction)
    {
        MoveAction.MoveRb(player.GetComponent<Rigidbody2D>(), speed,direction);
    }

    #region 初始化
    public bool Init()
    {
        player = GameObject.Find("Player");

        data = player.GetComponent<CharacterData>();
        return true;
    }
    public UniTask<bool> InitAysnc()
    {
        return UniTask.FromResult(true);

    }
    #endregion

    #region 生命周期
    public void OnFixUpdate()
    {
        
    }

    public void OnLateUpdate()
    {
        
    }

    public void OnStart()
    {
        
    }

    public void OnUpdate()
    {
        //GameCore.Instance.GetManager<PlayerManager>().playerGo.transform.position = player.transform.position;
    }

    public void Release()
    {

    }
    #endregion
    
}
