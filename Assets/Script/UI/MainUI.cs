using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    public Button startGame;
    public Button exitGame;
    public Button option;
    private void Awake()
    {
        startGame.onClick.AddListener(OnStart);
        exitGame.onClick.AddListener(OnExit);
        option.onClick.AddListener(OnOption);
    }
    private void OnStart()
    {
        UIManager.Instance.CloseUI(0);
        UIManager.Instance.ShowUI("BattleUI");
    }
    private void OnExit()
    {
        Application.Quit();
    }

    private void OnOption()
    {

    }
}
