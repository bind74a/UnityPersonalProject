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
