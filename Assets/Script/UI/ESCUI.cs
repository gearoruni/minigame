using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESCUI : MonoBehaviour
{
    public Button quit, continueGame, backMain;
    private void Awake()
    {
        quit.onClick.AddListener(OnQuit);
        continueGame.onClick.AddListener(OnContinueGame);
        backMain.onClick.AddListener(OnBackMain);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
    private void OnContinueGame()
    {
        EntityManager.Instance.isStop = false;
        UIManager.Instance.CloseUI<ESCUI>();
    }
    private void OnBackMain()
    {
        // PlayerBaseData.Instance.Clear();
        // GoComponent gocmp;
        // foreach (var entity in EntityManager.Instance.entities)
        // {
        //     if (entity.Value == null) continue;
        //     gocmp = (GoComponent)entity.Value.GetComponent("GoComponent");
        //     ObjectPool.Instance.ReturnObjectToPool(gocmp.name, gocmp.go);
        // }
        // UIManager.Instance.CloseUI<ESCUI>();
        // UIManager.Instance.CloseUI<BattleUI>();
        // UIManager.Instance.ShowUI("MainUI");
        // EntityManager.Instance.isStop = false;
        PlayerBaseData.Instance.ReStartToKaimi();
        OnContinueGame();
    }
}
