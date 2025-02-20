using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button overexitButton;

    ButtonManager ButtonManager;

    public override void Init(UIMnager uiMnager)
    {
        base.Init(uiMnager);

        restartButton.onClick.AddListener(OnClickRestartButton);
        overexitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        //리스트트 버튼 연동
        ButtonManager.RestartGame();
    }

    public void OnClickExitButton()
    {
        ButtonManager.RetunLobby();
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}
