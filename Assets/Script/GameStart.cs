using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void Awake()
    {
        ObjectPoolManager.Instance.Init();
    }

    // Start is called before the first frame update
    async void Start()
    {
        var go = new GameObject("gameCore");
        DontDestroyOnLoad(go);
        var core = go.AddComponent<GameCore>();
        await core.Active();
        //--------------------Ctrl---------------------------------------------
        PlayerInputCtrl playerInputCtrl = new PlayerInputCtrl();
        core.RegisterModel(playerInputCtrl);

        PlayerMoveCtrl playerMoveCtrl = new PlayerMoveCtrl();
        core.RegisterModel(playerMoveCtrl);

        //--------------------Manager------------------------------------------

    }
}
