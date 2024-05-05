using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void Awake()
    {
        ObjectPoolManager.Instance.Init();
        Preloader.Instance.Init();
    }

    // Start is called before the first frame update
    async void Start()
    {
        var core =  GameCore.Instance.Init();
        //--------------------Manager------------------------------------------
        PlayerManager playerManager = new PlayerManager();
        core.RegisterManager(playerManager);

        //--------------------Ctrl---------------------------------------------
        PlayerInputCtrl playerInputCtrl = new PlayerInputCtrl();
        core.RegisterModel(playerInputCtrl);

        PlayerMoveCtrl playerMoveCtrl = new PlayerMoveCtrl();
        core.RegisterModel(playerMoveCtrl);

        WeaponDirCtrl weaponDirCtl = new WeaponDirCtrl();
        core.RegisterModel(weaponDirCtl);

        WeaponFireCtrl weaponFireCtl = new WeaponFireCtrl();
        core.RegisterModel(weaponFireCtl);

        TimerCtrl timerCtrl = new TimerCtrl();
        core.RegisterModel(timerCtrl);
        //---------------------------------------------------------------------
        await core.Active();
    }
}
