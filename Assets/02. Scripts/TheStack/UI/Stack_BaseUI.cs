using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stack_BaseUI : MonoBehaviour
{
    protected Stack_UIManager uiManager;

    public virtual void Init(Stack_UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract Stack_UIState GetUIState();
    public void SetActive(Stack_UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}