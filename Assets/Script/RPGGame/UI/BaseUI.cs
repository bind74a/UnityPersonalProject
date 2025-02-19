using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UIMnager uiMnager;
    public virtual void Init(UIMnager uiMnager)
    {

    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState states)
    {
        this.gameObject.SetActive(GetUIState() == states);
    }
}
