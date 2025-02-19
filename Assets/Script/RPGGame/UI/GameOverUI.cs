using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button overexitButton;

    public override void Init(UIMnager uiMnager)
    {
        base.Init(uiMnager);

        restartButton.onClick.AddListener();
    }

    
}
